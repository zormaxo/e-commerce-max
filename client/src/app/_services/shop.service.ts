import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { ICategory } from '../shared/models/category';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { of, ReplaySubject } from 'rxjs';
import { IProduct } from '../shared/models/product';
import { IAddress } from '../shared/models/address';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  categories: ICategory[];
  private customCategorySource = new ReplaySubject(1);
  customCategory$ = this.customCategorySource.asObservable();
  searchTerm: string; //relation between nav and productList

  constructor(private http: HttpClient) {}

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.brandId !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if (shopParams.categoryId !== 0) {
      params = params.append('typeId', shopParams.categoryId.toString());
    }

    if (shopParams.categoryName !== undefined) {
      params = params.append('categoryName', shopParams.categoryName);
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    if (shopParams.countyId) {
      params = params.append('countyId', shopParams.countyId);
    }

    if (shopParams.cityId) {
      params = params.append('cityId', shopParams.cityId);
    }

    if (shopParams.isNew !== undefined) {
      params = params.append('isNew', shopParams.isNew);
    }

    if (shopParams.userId !== undefined) {
      params = params.append('userId', shopParams.userId);
    }

    if (shopParams.getAllStatus !== undefined) {
      params = params.append('getAllStatus', shopParams.getAllStatus);
    }

    if (shopParams.currency !== undefined) {
      params = params.append('currency', shopParams.currency);
    }

    if (shopParams.maxValue !== undefined) {
      const maxValue = shopParams.maxValue.replaceAll('.', '');
      params = params.append('maxValue', maxValue);
    }

    if (shopParams.minValue !== undefined && shopParams.maxValue !== '0') {
      const minValue = shopParams.minValue.replaceAll('.', '');
      params = params.append('minValue', minValue);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.http.get(this.baseUrl + 'products', { observe: 'response', params }).pipe(
      map((response: any) => {
        return response.body.result;
      })
    );
  }

  getProductCounts(userId: number) {
    let params = new HttpParams();
    params = params.append('userId', userId);

    return this.http.get<unknown>(this.baseUrl + 'products/product-counts', { params: params });
  }

  getProduct(id: number) {
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }

  getCities() {
    return this.http.get<IAddress[]>(this.baseUrl + 'products/cities');
  }

  updateProduct(product: IProduct) {
    return this.http.post<number>(this.baseUrl + 'products/update-product/', product);
  }

  getCategories() {
    const pushChildCategories = (category: ICategory) => {
      if (category.childCategories) {
        category.childCategories.forEach((child) => {
          child.parent = category;
          this.categories.push(child);
          pushChildCategories(child);
        });
      }
    };

    if (this.categories === undefined) {
      return this.http.get(this.baseUrl + 'products/categories').pipe(
        map((asd : any) => {
          this.categories = asd.result.filter((x) => x.parent == null);
          this.categories.forEach((x) => {
            pushChildCategories(x);
          });
          return structuredClone(this.categories);
        })
      );
    } else {
      return of(structuredClone(this.categories));
    }
  }

  addCountToParents(selectedCategory: ICategory, count: number) {
    if (selectedCategory.parent) {
      selectedCategory.parent.count += count;
      this.addCountToParents(selectedCategory.parent, count);
    }
  }

  generateFilteredCategory(selectedCategoryId: number): void {
    let selectedCategory: ICategory;
    this.getCategories().subscribe((categories) => {
      selectedCategory = categories.find((x: { id: number }) => x.id == selectedCategoryId);
      const parentCategories = this.fillParentCategoryList(selectedCategory);
      this.customCategorySource.next({ selectedCategory, parentCategories });
    });
  }

  fillParentCategoryList(selectedCategory: ICategory): ICategory[] {
    const parentCategories = [];
    fillList(selectedCategory);
    return parentCategories;

    function fillList(selectedCategory: ICategory) {
      if (selectedCategory.parent) {
        parentCategories.unshift(selectedCategory.parent);
        fillList(selectedCategory.parent);
      }
    }
  }
}
