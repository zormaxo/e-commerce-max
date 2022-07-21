export interface IType {
  id: number;
  name: string;
  count: number;
  url: string;
  parent: IType;
  childCategories: IType[];
}
