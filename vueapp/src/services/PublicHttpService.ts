import axios, {
    type AxiosInstance,
  } from "axios";

  // const baseUrl = `/api/`;
  const baseApiUrl = import.meta.env.VITE_Api_Url;
  export const PublicHttpService: AxiosInstance = axios.create({
    baseURL: baseApiUrl,
    headers: {
      "Content-type": "application/json",
    },
  });

  export default function usePublicHttpService() {
    return PublicHttpService;
  }