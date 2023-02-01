import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { ICategory } from '../shared/models/category';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { Observable, of, ReplaySubject, Subject } from 'rxjs';
import { Product } from '../shared/models/product';
import { IAddress } from '../shared/models/address';
import { ApiResponse } from '../shared/models/api-response/api-response';
import { CategoryGroupCount } from '../shared/models/categoryGroupCount';
import { environment } from 'src/environments/environment';
import { LeftNavMode } from '../shared/enums/leftNavMode';
import { Member } from '../shared/models/member';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = environment.apiUrl;
  products: Product[] = [];
  categories: ICategory[] = [];
  cities: IAddress[] = [];
  pagination?: Pagination<Product[]>;
  shopParams = new ShopParams();
  productCache = new Map<string, Pagination<Product[]>>();

  private categoryWithParents = new ReplaySubject<{ selectedCategory: ICategory; parentCategories: ICategory[] }>(1);
  categoryWithParents$ = this.categoryWithParents.asObservable();

  private categoryWithCounts = new ReplaySubject<ICategory[]>(1);
  categoryWithCounts$ = this.categoryWithCounts.asObservable();

  categorySelected = new Subject<ICategory>();
  searchClicked = new Subject<ShopParams>();
  searchTerm: string; //relation between nav and productList

  //For base
  productAdded = new Subject<{
    allCategories: ICategory[];
    sCategory: ICategory;
    shopParams: ShopParams;
    mainCategoryName: string;
    mode: LeftNavMode;
    member: Member;
  }>();

  constructor(private http: HttpClient) {}

  getProducts(useCache = true): Observable<Pagination<Product[]>> {
    if (!useCache) this.productCache = new Map();

    if (this.productCache.size > 0 && useCache) {
      if (this.productCache.has(Object.values(this.shopParams).join('-'))) {
        this.pagination = this.productCache.get(Object.values(this.shopParams).join('-'));
        if (this.pagination) return of(this.pagination);
      }
    }

    const params: HttpParams = this.generateHttpParams(this.shopParams);

    return this.http.get<ApiResponse<Pagination<Product[]>>>(this.baseUrl + 'products', { params }).pipe(
      map((response) => {
        this.productCache.set(Object.values(this.shopParams).join('-'), response.result);
        this.pagination = response.result;
        return response.result;
      })
    );
  }

  getProduct(id: number) {
    const product = [...this.productCache.values()].reduce((acc, paginatedResult) => {
      return { ...acc, ...paginatedResult.data.find((x) => x.id === id) };
    }, {} as Product);

    if (Object.keys(product).length !== 0) return of(product);

    return this.http.get<ApiResponse<Product>>(this.baseUrl + 'products/' + id).pipe(
      map((response) => {
        return response.result;
      })
    );
  }

  // updateProduct(product: Product) {
  //   return this.http.post<number>(this.baseUrl + 'products/update-product/', product);
  // }

  AddOrRemoveFavourite(productId: number) {
    return this.http.post(this.baseUrl + 'products/add-remove-favourite/' + productId, {});
  }

  changeActiveStatus(product: Product) {
    return this.http.post<number>(this.baseUrl + 'products/change-active-status/', product);
  }

  setShopParams(params: ShopParams) {
    this.shopParams = params;
  }

  getShopParams() {
    return this.shopParams;
  }

  getProductCounts(userId: number) {
    let params = new HttpParams();
    params = params.append('userId', userId);

    return this.http.get<{ activeProducts: number; inactiveProducts: number; favourites: number }>(
      this.baseUrl + 'products/product-counts',
      {
        params: params,
      }
    );
  }

  getCities() {
    if (this.cities.length === 0) {
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

    if (this.categories.length === 0) {
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

  //Adds product counts to parent categories cumulatively. This metod requires both product and category results.
  calculateProductCountsByCategory(
    allCategories: ICategory[],
    categoryGroupCount: CategoryGroupCount[],
    isOverWrite = false
  ) {
    const addCountToParent = (selectedCategory: ICategory, count: number) => {
      if (selectedCategory.parent) {
        selectedCategory.parent.count += count;
        addCountToParent(selectedCategory.parent, count);
      }
    };

    allCategories.forEach((category) => (category.count = 0));
    categoryGroupCount.forEach((groupCount) => {
      const category = allCategories.find((x) => x.id == groupCount.categoryId);
      category.count = groupCount.count;
      addCountToParent(category, groupCount.count);
    });
    if (isOverWrite) {
      this.categoryWithCounts.next(allCategories);
    }
  }

  //Populate parent category list to use in leftNav and breadcrumb components
  fillParentCategoryList(selectedCategory: ICategory): ICategory[] {
    const parentCategories = [];
    if (selectedCategory) {
      fillList(selectedCategory);
    }

    this.categoryWithParents.next({ selectedCategory, parentCategories });
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

    if (shopParams.favourite !== undefined) {
      params = params.append('favourite', shopParams.favourite);
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
      .get<ApiResponse<Pagination<Product[]>>>(this.baseUrl + 'productsmachine', { observe: 'response', params })
      .pipe(
        map((response) => {
          return response.body.result;
        })
      );
  }

  getMaterialProducts(shopParams: ShopParams) {
    const params: HttpParams = this.generateHttpParams(shopParams);

    return this.http
      .get<ApiResponse<Pagination<Product[]>>>(this.baseUrl + 'productsmaterial', { observe: 'response', params })
      .pipe(
        map((response) => {
          return response.body.result;
        })
      );
  }

  getSemiFinishedProducts(shopParams: ShopParams) {
    const params: HttpParams = this.generateHttpParams(shopParams);

    return this.http
      .get<ApiResponse<Pagination<Product[]>>>(this.baseUrl + 'productssemifinished', { observe: 'response', params })
      .pipe(
        map((response) => {
          return response.body.result;
        })
      );
  }
}
