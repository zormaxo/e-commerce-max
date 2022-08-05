export interface ICategory {
  id: number;
  name: string;
  count: number;
  url: string;
  parent: ICategory;
  childCategories: ICategory[];
}
