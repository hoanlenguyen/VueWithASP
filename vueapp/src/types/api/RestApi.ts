import type { IIdentityModel } from "@/types/base/IdentityModel";

export default interface IRestApi<T extends IIdentityModel>{
  get(id: number | string): Promise<T>;
  post(item: T): Promise<T>;
  put(item: T): Promise<T>;
  delete(id: number): Promise<string> | null;
  getAll(filter: any): Promise<T[]>;
}
