import { CategoryProductCount } from './categoryGroupCount';
import { IProduct } from './product';

export interface IPagination {
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  categoryGroupCount: CategoryProductCount[];
  data: IProduct[];
}
