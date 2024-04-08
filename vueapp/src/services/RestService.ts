// import { HttpService } from '@/services/HttpService';
import { PublicHttpService } from '@/services/PublicHttpService';
import type { IIdentityModel } from '@/types/base/IdentityModel';
import type IRestApi from '@/types/api/RestApi';

export default function useRestApi<T extends IIdentityModel>(apiRoute: string) : IRestApi<T> {

  // const httpService = HttpService;
  const httpService = PublicHttpService;

  const setStatus = (entity: T | null, httpStatusCode?: number, eTag?: string) : T | null => {
    if(entity && httpStatusCode !== undefined) {
      entity.httpStatusCode = httpStatusCode;
      entity.eTag = eTag;
    }
    return entity;
  }

  const get = async (id: number): Promise<T | null> => {
    const response = await httpService.get<T>(`${apiRoute}${id}`);
    const eTag = response.headers['etag'];
    return setStatus(response?.data, response?.status, eTag);
  }

  const post = async (t: T): Promise<T | null> => {
    const response = await httpService.post(`${apiRoute}`, t);
    return setStatus(response?.data, response?.status, response?.headers['etag']);
  }

  const put = async (t: T) : Promise<T | null> => {
    const response = await httpService.put(`${apiRoute}${t.id}`, t);
    return setStatus(response?.data, response?.status, response?.headers['etag']);
  }

  const deleteId = async (id: number): Promise<string> => {
    const response = await httpService.delete(`${apiRoute}${id}`);
    return response.headers['etag'];
  }

  return {get, put, post, delete: deleteId} as IRestApi<T>
}