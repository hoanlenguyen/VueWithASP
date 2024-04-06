import useIdentityStore from '@/stores/identity';
import { redirectRoute } from '@/router';
import axios, { AxiosError, CanceledError, type AxiosInstance } from 'axios';
import { v4 as uuidv4 } from 'uuid';

const baseUrl = '/api/';
export const ClientId = uuidv4();

export const HttpService: AxiosInstance = axios.create({
  baseURL: baseUrl,
  headers: {
    'Content-type': 'application/json',
    'ClientId': ClientId,
  }
});

export const HttpStatusCodeConstants = {
  OK: 200,
  BadRequest: 400,
  Forbidden: 403,
  Conflict: 409,
  ServerError: 500
};

const handleError = async (error: AxiosError) => {
  if (error instanceof CanceledError) {
    console.log(error.message);
  }
  if (error.response?.status === 403) {
    redirectRoute();
    return;
  }
  if (error.response?.status === HttpStatusCodeConstants.Conflict && error.response?.data) {
    return error.response;
  }
  if (error.response?.status === HttpStatusCodeConstants.BadRequest && error.response?.data) {
    throw new Error(error.response.data.toString());
  }
  if (error.response?.status === HttpStatusCodeConstants.ServerError) {
    throw error;
  }
};

HttpService.interceptors.request.use(async (request) => {
  const identityStore = useIdentityStore();
  await identityStore.authorise();
  return request;
}, handleError);

HttpService.interceptors.response.use(async (response) => {
  return response;
}, handleError);

export default function useHttpService() {
  return HttpService;
};
