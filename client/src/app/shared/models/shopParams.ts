export class ShopParams {
  brandId = 0;
  categoryId = 0;
  categoryName: string;
  sort = 'name';
  pageNumber = 1;
  pageSize: number;
  search: string;
  isNew: boolean;
  minValue: string;
  maxValue: string;
  userId: number;
  getAllStatus: boolean;

  constructor(pageSize = 20, categoryName = undefined) {
    this.pageSize = pageSize;
    this.categoryName = categoryName;
  }
}
