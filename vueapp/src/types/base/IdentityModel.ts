import type { IHttpResponseModel } from '@/types/base/IHttpResponseModel';
import { v4 as uuidv4 } from 'uuid';

export interface IIdentityModel extends IHttpResponseModel {
  id: number;
  uuid: string;
  rowVersion: Uint8Array | null;
}

export class IdentityModel implements IIdentityModel {
  
  constructor(identityModel?: Partial<IIdentityModel>) {
    if(identityModel) {
      if(identityModel.id) {
        this.id = identityModel.id;
      }
  
      if(identityModel.uuid) {
        this.uuid = identityModel.uuid
      }
  
      if(identityModel.rowVersion && identityModel.rowVersion.length > 0) {
        this.rowVersion = identityModel.rowVersion
      }
    }
  }
  
  id = 0;
  uuid: string = uuidv4();
  rowVersion: Uint8Array | null = null;
  httpStatusCode?: number | null = null;
  eTag?: string | null = null;
}
