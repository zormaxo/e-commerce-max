export class ShopParams {
  brandId = 0;
  typeId = 0;
  sort = 'name';
  pageNumber = 1;
  pageSize = 50;
  search: string;
  isNew: boolean;
  minValue: string;
  maxValue: string;
  userId: number;
  getAll: boolean;

  constructor(pageSize = 50) {
    this.pageSize = pageSize;
  }
}
