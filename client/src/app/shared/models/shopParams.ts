import { CurrencyType } from './currency';

export class ShopParams {
  categoryId: number;
  categoryName: string;
  sort = 'name';
  pageNumber = 1;
  pageSize: number;
  search: string;
  isNew: boolean;
  minValue: string;
  maxValue: string;
  cityId: number;
  countyId: number;
  userId: number;
  currency: CurrencyType = CurrencyType.TL;
  getAllStatus: boolean;

  constructor(pageSize = 20, categoryName = undefined) {
    this.pageSize = pageSize;
    this.categoryName = categoryName;
  }
}
