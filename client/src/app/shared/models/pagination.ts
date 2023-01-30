import { CategoryGroupCount } from './categoryGroupCount';

export interface Pagination<T> {
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  categoryGroupCount: CategoryGroupCount[];
  data: T;
}
