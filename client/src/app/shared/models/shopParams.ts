export class ShopParams {
  brandId = 0;
  typeId = 0;
  sort = 'name';
  pageNumber = 1;
  pageSize: number;
  search: string;
  isNew: boolean;
  minValue: string;
  maxValue: string;
  userId: number;
  getAllStatus: boolean;

  childCategoryIds = [];

  constructor(pageSize = 20) {
    this.pageSize = pageSize;
  }
}
