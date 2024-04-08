import type IRestApi from '@/types/api/RestApi';
import useRestApi from '@/services/RestService';
import type { IProducton } from '@/types/Production';

export default function useProductonApi(): IRestApi<IProducton> {
  const restApi = useRestApi<IProducton>('/product/');

  return {
    get: restApi.get,
    put: restApi.put,
    post: restApi.post,
    delete: restApi.delete
  } as IRestApi<IProducton>;
}
