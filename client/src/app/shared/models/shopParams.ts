import { createLogger } from '@microsoft/signalr/dist/esm/Utils';
import { CurrencyType } from './currency';

export class ShopParams {
  categoryId: number;
  categoryName: string;
  sort = 'name';
  pageNumber = 1;
  pageSize: number;
  _search: string;
  isNew: boolean;
  minValue: string;
  maxValue: string;
  cityId = 0; //when reset search we need this as zero
  countyId = 0;
  userId: number;
  currency: CurrencyType = CurrencyType.TL;
  getAllStatus: boolean;
  favourite: boolean;

  constructor(pageSize = 20, categoryName = undefined) {
    this.pageSize = pageSize;
    this.categoryName = categoryName;
  }

  public get search() {
    return this._search;
  }

  public set search(omer) {

    console.log('deneme  ' + omer);
    // console.trace();
    this._search = omer;
  }
}
