// import useIdentityStore from "@/stores/identity";
import router, { getRedirectRoute } from "@/router";
import axios, { AxiosError, CanceledError, type AxiosInstance, } from "axios";
import type { InjectionKey } from "vue";
import injectStrict from "./InjectStrict";
import { v4 as uuidv4 } from 'uuid';

const baseUrl = `/api/`;
export const ClientId = uuidv4();

export const HttpService: AxiosInstance = axios.create({
  baseURL: baseUrl,
  headers: {
    "Content-type": "application/json",
    "ClientId": ClientId,
  },
});

const handleError = async (error: AxiosError) => {
  if (error instanceof CanceledError) {
    console.log(error.message);
  }
  if (error instanceof CanceledError || error.response?.status === 403) {
    const routeLocation = getRedirectRoute(router.currentRoute.value) ?? '/';
    router.push(routeLocation);
    return;
  }
  if (error.response?.status === 400 && error.response?.data) {
    throw new Error(error.response.data.toString());
  }
  if (error.response?.status === 500) {
    throw error;
  }
};

HttpService.interceptors.request.use( async (request) => {
  // const identityStore = useIdentityStore();
  // await identityStore.authorise();
  return request;
}, handleError);

HttpService.interceptors.response.use( async (response) => {
  return response;
}, handleError);

export const AxiosKey: InjectionKey<AxiosInstance> = Symbol('HttpService');

export default function useHttpService() {
  return injectStrict(AxiosKey);
}
