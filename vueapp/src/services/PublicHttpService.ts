import axios, {
    type AxiosInstance,
  } from "axios";

  const baseUrl = `/api/`;

  export const PublicHttpService: AxiosInstance = axios.create({
    baseURL: baseUrl,
    headers: {
      "Content-type": "application/json",
    },
  });

  export default function usePublicHttpService() {
    return PublicHttpService;
  }