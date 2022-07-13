export class ShopParams {
  brandId = 0;
  typeId = 0;
  sort = 'name';
  pageNumber = 1;
  pageSize = 50;
  search: string;
  isNew: boolean;
  minValue = 0;
  maxValue = 0;

  constructor(pageSize = 50) {
    this.pageSize = pageSize;
  }
}
