import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { ICategory } from '../shared/models/category';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { Observable, of, ReplaySubject, Subject } from 'rxjs';
import { IProduct } from '../shared/models/product';
import { IAddress } from '../shared/models/address';
import { ApiResponse } from '../shared/models/api-response/api-response';
import { CategoryGroupCount } from '../shared/models/categoryGroupCount';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = environment.apiUrl;
  ocelotUrl = environment.ocelotUrl;
  categories: ICategory[];
  cities: IAddress[];

  private categoryWithParents = new ReplaySubject<{ selectedCategory: ICategory; parentCategories: ICategory[] }>(1);
  categoryWithParents$ = this.categoryWithParents.asObservable();
  searchClicked = new Subject<ShopParams>();
  searchTerm: string; //relation between nav and productList

  constructor(private http: HttpClient) {}

  getProducts(shopParams: ShopParams) {
    const params: HttpParams = this.generateHttpParams(shopParams);

    return this.http.get(this.ocelotUrl + 'products', { observe: 'response', params }).pipe(
      map((response: HttpResponse<ApiResponse<IPagination<IProduct[]>>>) => {
        return response.body.result;
      })
    );
  }

  getProductCounts(userId: number) {
    let params = new HttpParams();
    params = params.append('userId', userId);

    return this.http.get<{ activeProducts: number; inactiveProducts: number }>(
      this.baseUrl + 'products/product-counts',
      {
        params: params,
      }
    );
  }

  getProduct(id: number) {
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }

  getCities() {
    if (!this.cities) {
      return this.http.get<ApiResponse<IAddress[]>>(this.baseUrl + 'shared/cities').pipe(
        map((response: ApiResponse<IAddress[]>) => {
          this.cities = response.result;
          return this.cities;
        })
      );
    } else {
      return of(this.cities);
    }
  }

  updateProduct(product: IProduct) {
    return this.http.post<number>(this.baseUrl + 'products/update-product/', product);
  }

  changeActiveStatus(product: IProduct) {
    return this.http.post<number>(this.baseUrl + 'products/change-active-status/', product);
  }

  getCategories(): Observable<ICategory[]> {
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
      return this.http.get(this.baseUrl + 'categories').pipe(
        map((response: ApiResponse<ICategory[]>) => {
          this.categories = response.result.filter((x) => x.parent == null);
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

  //Adds product counts to parent categories cumulatively
  addCountToParents(allCategories: ICategory[], categoryGroupCount: CategoryGroupCount[]) {
    const addCount = (selectedCategory: ICategory, count: number) => {
      if (selectedCategory.parent) {
        selectedCategory.parent.count += count;
        addCount(selectedCategory.parent, count);
      }
    };

    allCategories.forEach((category) => (category.count = 0));
    categoryGroupCount.forEach((groupCount) => {
      const category = allCategories.find((x) => x.id == groupCount.categoryId);
      category.count = groupCount.count;
      addCount(category, groupCount.count);
    });
  }

  generateBreadcrumb(selectedCategoryId: number): void {
    let selectedCategory: ICategory;
    this.getCategories().subscribe((categories) => {
      selectedCategory = categories.find((x: { id: number }) => x.id == selectedCategoryId);
      const parentCategories = this.fillParentCategoryList(selectedCategory);
      this.categoryWithParents.next({ selectedCategory, parentCategories });
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

  private generateHttpParams(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.categoryId !== undefined) {
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

    return params;
  }

  getMachineProducts(shopParams: ShopParams) {
    const params: HttpParams = this.generateHttpParams(shopParams);

    return this.http
      .get<ApiResponse<IPagination<IProduct[]>>>(this.baseUrl + 'productsmachine', { observe: 'response', params })
      .pipe(
        map((response) => {
          return response.body.result;
        })
      );
  }

  getMaterialProducts(shopParams: ShopParams) {
    const params: HttpParams = this.generateHttpParams(shopParams);

    return this.http
      .get<ApiResponse<IPagination<IProduct[]>>>(this.baseUrl + 'productsmaterial', { observe: 'response', params })
      .pipe(
        map((response) => {
          return response.body.result;
        })
      );
  }

  getSemiFinishedProducts(shopParams: ShopParams) {
    const params: HttpParams = this.generateHttpParams(shopParams);

    return this.http
      .get<ApiResponse<IPagination<IProduct[]>>>(this.baseUrl + 'productssemifinished', { observe: 'response', params })
      .pipe(
        map((response) => {
          return response.body.result;
        })
      );
  }
}
