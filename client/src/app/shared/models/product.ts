import { ICategory } from "./category";

export interface IProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  pictureUrl: string;
  productType: string;
  productBrand: string;
  created: Date;
  city: string;
  county: string;
  isActive: boolean;
  category: ICategory;
}
