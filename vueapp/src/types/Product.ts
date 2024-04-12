import { IdentityModel, type IIdentityModel } from '@/types/base/IdentityModel';
// import type { IProductionContact } from './productonContact';

export interface IProduct extends IIdentityModel {
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

export class Product extends IdentityModel implements IProduct {

  // static hasAddress(producton: IProduction) : boolean {
  //   return !!producton.addressLine1;
  // }

  constructor(product?: Partial<IProduct>) {
    super(product);
    Object.assign(this, product);
  }

  name: string | null = null;
  description: string | null = null;
  shortDescription: string | null = null;
  avatarUrl: string | null = null;
  price: number | null = null;
  categoryId: number | null = null;
  brandId: number | null = null;
}