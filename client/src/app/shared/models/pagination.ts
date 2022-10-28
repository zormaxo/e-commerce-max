import { CategoryGroupCount } from './categoryGroupCount';

export interface IPagination<T> {
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  categoryGroupCount: CategoryGroupCount[];
  data: T;
}
