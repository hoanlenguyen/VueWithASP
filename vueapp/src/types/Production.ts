import { IdentityModel, type IIdentityModel } from '@/types/base/IdentityModel';
// import type { IProductonContact } from './productonContact';

export interface IProducton extends IIdentityModel {
  name: string | null;
  description: string | null;
  shortDescription: string | null;
  avatarUrl: string | null;
  price: number | null;
  categoryId: number | null;
  brandId: number | null;

  // [ForeignKey(nameof(CategoryId))]
  // public virtual ProductCategory? Category { get; set; }

  // [ForeignKey(nameof(BrandId))]
  // public virtual Brand? Brand { get; set; }

  // public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
  // public virtual ICollection<ProductTag> ProductTags { get; set; } = new HashSet<ProductTag>();
}

export class Production extends IdentityModel implements IProducton {

  // static hasAddress(producton: IProducton) : boolean {
  //   return !!producton.addressLine1;
  // }

  constructor(producton?: Partial<IProducton>) {
    super(producton);
    Object.assign(this, producton);
  }

  name: string | null = null;
  description: string | null = null;
  shortDescription: string | null = null;
  avatarUrl: string | null = null;
  price: number | null = null;
  categoryId: number | null = null;
  brandId: number | null = null;
}