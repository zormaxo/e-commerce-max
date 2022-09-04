import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { ICategory } from './category';

export interface IProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  priceText: string;
  pictureUrl: string;
  productType: string;
  productBrand: string;
  created: Date;
  city: string;
  county: string;
  isActive: boolean;
  category: ICategory;
  photos: Photo[];
  user: Member;
}
