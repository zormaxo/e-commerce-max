import { ICategory } from './category';
import { Member } from './member';
import { Photo } from './photo';

export interface IProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  priceText: string;
  pictureUrl: string;
  productType: string;
  created: Date;
  city: string;
  county: string;
  isActive: boolean;
  // category: ICategory;
  categoryId: number;
  photos: Photo[];
  user: Member;
  isFavourite: boolean;
}
