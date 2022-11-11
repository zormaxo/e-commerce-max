import { Directive, Injector, OnInit } from '@angular/core';
import { Navigation, ActivatedRoute, Router } from '@angular/router';
import { ICategory } from './shared/models/category';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/product';
import { ShopParams } from './shared/models/shopParams';
import { ShopService } from './shop/shop.service';

@Directive()
export abstract class AppProductBaseClass implements OnInit {
  shopParams: ShopParams = new ShopParams(10);
  filterShopParams: ShopParams; //This button has been added to add filter buttons after pressing the search button.
  products: IProduct[];
  totalCount: number;
  allCategories: ICategory[];
  categoryName: string;
  mainCategoryName: string;
  selectedCategory: ICategory;
  navigation: Navigation;

  route: ActivatedRoute;
  router: Router;
  shopService: ShopService;

  constructor(injector: Injector) {
    this.route = injector.get(ActivatedRoute);
    this.router = injector.get(Router);
    this.shopService = injector.get(ShopService);

    const navigation = this.router.getCurrentNavigation();
    this.shopParams.search = navigation?.extras?.state?.searchTerm;
    this.shopParams.cityId = navigation?.extras?.state?.cityId ?? 0;
    this.shopParams.countyId = navigation?.extras?.state?.countyId ?? 0;

    if (this.shopParams.search || this.shopParams.cityId || this.shopParams.countyId) {
      this.filterShopParams = this.shopParams;
    }
  }

  ngOnInit(): void {
    this.route.params.subscribe(() => {
      this.shopParams.categoryName = this.categoryName = this.route.snapshot.url[0].path;
      this.mainCategoryName = this.route.parent.parent.snapshot.url[0].path;
      this.getCategoriesThenProducts();
    });

    this.shopService.searchClicked.subscribe((shopParams: ShopParams) => {
      this.shopParams = shopParams;
      this.filterShopParams = structuredClone(shopParams);
      this.getProducts2();
    });
  }

  getCategoriesThenProducts() {
    this.shopService.getCategories().subscribe((categories) => {
      this.allCategories = categories;
      this.selectedCategory = this.allCategories.find((x) => x.url == this.shopParams.categoryName);
      if (!this.selectedCategory) {
        this.router.navigateByUrl('/notfound');
      }
      this.getProducts2();
    });
  }

  onPageChanged(event: number) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts2();
    }
  }

  onHeaderClicked(sortText: string) {
    this.shopParams.sort = sortText;
    this.getProducts2();
  }

  onRemoveFilterClick(event: ShopParams) {
    this.shopParams = structuredClone(event);
    this.getProducts2();
  }

  getProducts2() {
    this.getProducts().subscribe((productResponse: IPagination<IProduct[]>) => {
      this.products = productResponse.data;
      this.shopParams.pageNumber = productResponse.pageIndex;
      this.shopParams.pageSize = productResponse.pageSize;
      this.totalCount = productResponse.totalCount;

      this.shopService.addCountToParents(this.allCategories, productResponse.categoryGroupCount);

      this.shopService.productAdded.next({
        allCategories: this.allCategories,
        sCategory: this.selectedCategory,
        shopParams: this.shopParams,
        mainCategoryName : this.mainCategoryName,
      });
    });
  }

  abstract getProducts();
}
