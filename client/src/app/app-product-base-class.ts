import { Directive, Injector, OnDestroy, OnInit } from '@angular/core';
import { Navigation, ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { MembersService } from './core/services/members.service';
import { LeftNavMode } from './shared/enums/leftNavMode';
import { ICategory } from './shared/models/category';
import { Member } from './shared/models/member';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/product';
import { ShopParams } from './shared/models/shopParams';
import { ShopService } from './shop/shop.service';

@Directive()
export abstract class AppProductBaseClass implements OnInit, OnDestroy {
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
  memberService: MembersService;
  leftNavMode = LeftNavMode.AllProducts;
  member: Member;
  subs: Subscription;
  subs2: Subscription;

  constructor(injector: Injector) {
    this.route = injector.get(ActivatedRoute);
    this.router = injector.get(Router);
    this.shopService = injector.get(ShopService);
    this.memberService = injector.get(MembersService);

    const navigation = this.router.getCurrentNavigation();
    this.shopParams.search = navigation?.extras?.state?.searchTerm;
    this.shopParams.cityId = navigation?.extras?.state?.cityId ?? 0;
    this.shopParams.countyId = navigation?.extras?.state?.countyId ?? 0;

    if (this.shopParams.search || this.shopParams.cityId || this.shopParams.countyId) {
      this.filterShopParams = this.shopParams;
    }
  }
  ngOnDestroy(): void {
    this.subs.unsubscribe();
    this.subs2.unsubscribe();
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

    this.subs2 = this.shopService.searchClicked.subscribe((shopParams: ShopParams) => {
      this.shopParams = shopParams;
      this.filterShopParams = structuredClone(shopParams);
      this.getProducts2();
    });

    this.subs = this.shopService.categorySelected.subscribe((category: ICategory) => {
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
      // if (!this.selectedCategory) {
      //   this.router.navigateByUrl('/notfound');
      // } else {
      //   this.getProducts2();
      // }

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

      this.shopService.calculateProductCountsByCategory(this.allCategories, productResponse.categoryGroupCount);

      this.shopService.productAdded.next({
        allCategories: this.allCategories,
        sCategory: this.selectedCategory,
        shopParams: this.shopParams,
        mainCategoryName: this.mainCategoryName,
        mode: this.leftNavMode,
        member: this.member,
      });
    });
  }

  abstract getProducts();
}
