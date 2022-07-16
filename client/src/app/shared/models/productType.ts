export interface IType {
  id: number;
  name: string;
  count: number;
  parent: IType;
  childCategories: IType[];
}
