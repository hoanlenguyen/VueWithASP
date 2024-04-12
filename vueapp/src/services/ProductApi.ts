import type IRestApi from '@/types/api/RestApi';
import useRestApi from '@/services/RestService';
import type { IProduct as IProduct } from '@/types/Product';

export default function useProductApi(): IRestApi<IProduct> {
  const restApi = useRestApi<IProduct>('/products/');

  return {
    get: restApi.get,
    put: restApi.put,
    post: restApi.post,
    delete: restApi.delete,
    getAll: restApi.getAll
  } as IRestApi<IProduct>;
}
