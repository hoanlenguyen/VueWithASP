import { defineStore } from 'pinia';
import type { AxiosError } from 'axios'
import equal from 'fast-deep-equal'
import axios from 'axios';
import { resetAllStores } from './plugins/resetState';
import type { RouteLocationNormalized } from 'vue-router';
import { useCookies } from 'vue3-cookies';
import { UserType } from '@/areas/profile/constants/UserType';
import type IPermission from '@/types/identity/Permission';
import { PermissionValues } from '@/types/identity/Permission';
import { UserInfo, type IUserInfo } from '@/types/identity/UserInfo';
import type IOrganisation from '@/types/identity/Organisation';
import { useConfigService } from '@/services/ConfigService';
import usePlanService from '@/areas/engage/services/planService';

export const identityStoreId = 'identity';
const baseUrl = import.meta.env.VITE_Base_Url;
const platformUrl = import.meta.env.VITE_Platform_Url;

const loginUrl = `${baseUrl}/identity/account/login?returnurl=${encodeURIComponent(platformUrl + '/#/verify')}`;
const logoutUrl = `${baseUrl}/identity/account/logout`;

export const IdentityConstants = {
  SchroleIdentity : '.Schrole.Identity',
  ImpersonateUserEmail : '.Schrole.ImpersonateUserEmail',
  CurrentOrganisationId: '.Schrole.CurrentOrganisationId',
}

interface IIdentityState {
  token?: string,
  cookie?: string,
  userInfo: IUserInfo,
  permissions: IPermission[],
}

const IdentityHttpService = axios.create({
  headers: {
    'Content-type': 'application/json',
  },
});

const useIdentityStore = defineStore({
  id: identityStoreId,
  state: (): IIdentityState => ({
    userInfo: new UserInfo(),
    cookie: undefined,
    token: undefined,
    permissions: [],
  } as IIdentityState),
  getters: {
    isImpersonatingUser: (state) => state.userInfo.authenticated && state.userInfo.identityEmail !== state.userInfo.email,
    isOrganisationUser: (state) => state.userInfo.authenticated && state.userInfo.userType === UserType.Organisation,
    currentOrganisation: (state) => {
      const organisations = state.userInfo?.organisations;
      return organisations && organisations.length > 0 ? organisations.find((o: IOrganisation) => o.id === state.userInfo?.currentOrganisationId) : undefined;
    },
    isGroupOrganisation(): boolean {
      return this.currentOrganisation?.isGroupSchool || false;
    },
    isChildOrganisation(): boolean {
      return this.currentOrganisation?.parentOrganisationId !== null;
    },
    requiresGst: (state) => !!(state.userInfo.requiresGst),
    hasPermission: (state) => (permissionValue: PermissionValues | PermissionValues[]) : boolean => {
      const resultPermission = state.permissions.find(value => Array.isArray(permissionValue) ? permissionValue.includes(value.id) : value.id === permissionValue);
      return !!(resultPermission?.isEnabled);
    },
    hasEngageOnboardingPermission() : boolean {
      return this.hasPermission(PermissionValues.EngageOnboarding)
    }
  },
  actions: {
    async authorise(to: RouteLocationNormalized | undefined = undefined) : Promise<boolean> {
      try {
        const { cookies } = useCookies();
        const currentCookie = cookies.get(IdentityConstants.SchroleIdentity);
        const impersonateUserEmail = cookies.get(IdentityConstants.ImpersonateUserEmail);
        const currentOrganisationId = cookies.isKey(IdentityConstants.CurrentOrganisationId)
          ? Number(cookies.get(IdentityConstants.CurrentOrganisationId))
          : null;

          //if we were authenticated and any of these things have changed then its a new we
        const identityChanged = this.userInfo.authenticated === true && 
          ((this.isImpersonatingUser && this.userInfo.email != impersonateUserEmail) || 
          (!this.isImpersonatingUser && !!impersonateUserEmail) || 
          this.userInfo.currentOrganisationId !== currentOrganisationId);

        //the first time we come in our identity hasn't changed, but we need to load userinfo  
        if(this.userInfo.authenticated === false || identityChanged || this.cookie === undefined || this.cookie != currentCookie) {
          const result = await IdentityHttpService.get<IUserInfo>(`/api/identity/userinfo`);
          this.cookie = currentCookie;        

          const wasAuthenticated = this.userInfo.authenticated;
          if(wasAuthenticated) {
            window.location.reload();
          } else {
            Object.assign(this.userInfo, new UserInfo(result.data, true));
            //reset stale data
            resetAllStores();
            if (this.isOrganisationUser && this.userInfo.currentOrganisationId != null) {              
              this.setCurrentOrganisation(this.userInfo.currentOrganisationId);
            } else if (currentOrganisationId != null) {
              cookies.remove(IdentityConstants.CurrentOrganisationId);
            }
            await this.fetchPermission();
            const configService = useConfigService();
            await configService.getConfig();
            if (this.hasEngageOnboardingPermission && !this.isOrganisationUser) {
              const planService = usePlanService();
              await planService.getUserPlans(this.userInfo.id);
            }
          }          
        }
        return true;
      }
      catch (error: unknown) {
        const axiosError = error as AxiosError | undefined;
        if (axiosError) {
          if (axiosError.response?.status === 401) {
            const toHref = to as RouteLocationNormalized & { href: string; };
            if (toHref && toHref.href) {
              const returnUrl = encodeURIComponent(`${platformUrl}/${toHref.href}`)
              window.location.href = `${baseUrl}/identity/account/login?returnurl=${returnUrl}`;
            }
            else {
              window.location.href = loginUrl;
            }
          }
          return false;
        }
      }
      return true;
    },
    async logout() {
      window.location.href = logoutUrl;
    },
    async revertImpersonation() {
      await IdentityHttpService.post('/api/Account/RevertImpersonateUser');
      await this.authorise();
    },
    async setCurrentOrganisation(currentOrganisationId : Number) {      
      await IdentityHttpService.post(`/api/school/setcurrentschool/${currentOrganisationId}/`);
      await this.authorise();
    },
    async fetchPermission() {
      const result = await IdentityHttpService.get<IPermission[]>(`/api/user/permissions`);
      this.permissions = result.data || [];
    },
    setDisplayName(displayName: string) {
      this.userInfo.displayName = displayName;
    }
  },
});

export default useIdentityStore;
