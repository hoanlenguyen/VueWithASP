import type { IIdentityModel } from "@/types/base/IdentityModel";

export default interface IRestApi<T extends IIdentityModel>{
  get(id: number | string): Promise<T>;
  post(order: T): Promise<T>;
  put(order: T): Promise<T>;
  delete(id: number): Promise<string> | null;
}
