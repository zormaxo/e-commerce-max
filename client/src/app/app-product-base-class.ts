import { Directive, Injector, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { MembersService } from './member-profile/members.service';
import { LeftNavMode } from './shared/enums/leftNavMode';
import { ICategory } from './shared/models/category';
import { Member } from './shared/models/member';
import { Pagination } from './shared/models/pagination';
import { Product } from './shared/models/product';
import { ShopParams } from './shared/models/shopParams';
import { ShopService } from './shop/shop.service';

@Directive()
export abstract class AppProductBaseClass implements OnInit, OnDestroy {
  shopParams: ShopParams;
  filterShopParams: ShopParams; //This button has been added to add filter buttons after pressing the search button.
  products: Product[];
  totalCount: number;
  allCategories: ICategory[];
  categoryName: string;
  mainCategoryName: string;
  selectedCategory: ICategory;
  // navigation: Navigation;

  route: ActivatedRoute;
  router: Router;
  shopService: ShopService;
  membersService: MembersService;
  leftNavMode = LeftNavMode.AllProducts;
  member: Member;
  categorySelectedSubs: Subscription;
  searchClickedSubs: Subscription;

  constructor(injector: Injector) {
    this.route = injector.get(ActivatedRoute);
    this.router = injector.get(Router);
    this.shopService = injector.get(ShopService);
    this.membersService = injector.get(MembersService);

    this.shopParams = this.shopService.getShopParams();

    // const navigation = this.router.getCurrentNavigation();
    // this.shopParams.search = navigation?.extras?.state?.searchTerm;
    // this.shopParams.cityId = navigation?.extras?.state?.cityId ?? 0;
    // this.shopParams.countyId = navigation?.extras?.state?.countyId ?? 0;

    if (this.shopParams.search || this.shopParams.cityId || this.shopParams.countyId) {
      this.filterShopParams = this.shopParams;
    }
  }

  ngOnInit(): void {
    this.route.params.subscribe(() => {
      const path = this.route.snapshot.url[0].path;
      if (isNaN(+path)) {
        this.shopParams.categoryName = this.categoryName = this.route.snapshot.url[0].path;
        this.mainCategoryName = this.route.parent.parent.snapshot.url[0]
          ? this.route.parent.parent.snapshot.url[0].path
          : this.route.parent.snapshot.url[0].path;
      }

      this.getCategoriesThenProducts();
    });

    this.searchClickedSubs = this.shopService.searchClicked.subscribe((shopParams: ShopParams) => {
      this.shopParams = shopParams;
      this.filterShopParams = structuredClone(shopParams);
      this.getProductsFromInherited();
    });

    this.categorySelectedSubs = this.shopService.categorySelected.subscribe((category: ICategory) => {
      if (category) {
        this.shopParams.categoryName = category.url;
      } else {
        this.shopParams.categoryName = undefined;
      }

      this.getCategoriesThenProducts();
    });
  }

  getCategoriesThenProducts() {
    this.shopService.getCategories().subscribe((categories) => {
      this.allCategories = categories;
      this.selectedCategory = this.allCategories.find((x) => x.url == this.shopParams.categoryName);
      this.getProductsFromInherited();
    });
  }

  onPageChanged(event: number) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProductsFromInherited();
    }
  }

  onHeaderClicked(sortText: string) {
    this.shopParams.sort = sortText;
    this.getProductsFromInherited();
  }

  onRemoveFilterClick(event: ShopParams) {
    this.shopParams = structuredClone(event);
    this.getProductsFromInherited();
  }

  getProductsFromInherited() {
    this.getProducts().subscribe((productResponse: Pagination<Product[]>) => {
      this.products = productResponse.data;
      this.shopParams.pageNumber = productResponse.pageIndex;
      this.shopParams.pageSize = productResponse.pageSize;
      this.totalCount = productResponse.totalCount;

      this.shopService.calculateProductCountsByCategory(this.allCategories, productResponse.categoryGroupCount);

      this.shopService.productsFetched.next({
        allCategories: this.allCategories,
        selectedCategory: this.selectedCategory,
        shopParams: this.shopParams,
        mainCategoryName: this.mainCategoryName,
        leftNavMode: this.leftNavMode,
        member: this.member,
      });
    });
  }

  ngOnDestroy(): void {
    if (this.categorySelectedSubs) {
      this.categorySelectedSubs.unsubscribe();
    }
    if (this.searchClickedSubs) {
      this.searchClickedSubs.unsubscribe();
    }
  }

  abstract getProducts();
}
