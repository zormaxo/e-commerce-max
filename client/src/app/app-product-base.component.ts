import { Component, Directive, Injector, OnInit } from '@angular/core';
import { Navigation, ActivatedRoute, Router } from '@angular/router';
import { IAddress } from './shared/models/address';
import { ICategory } from './shared/models/category';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/product';
import { ShopParams } from './shared/models/shopParams';
import { ShopService } from './_services/shop.service';

import { Inject } from '@angular/core';

@Directive()
export abstract class AppProductBaseComponent implements OnInit {
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
      this.mainCategoryName = this.route.parent.snapshot.url[0].path;
      this.getCategoriesThenProducts();
    });

    this.shopService.searchClicked.subscribe((shopParams: ShopParams) => {
      this.shopParams = shopParams;
      this.filterShopParams = structuredClone(shopParams);
      this.getProducts();
    });
  }

  getCategoriesThenProducts() {
    this.shopService.getCategories().subscribe((categories) => {
      this.allCategories = categories;
      this.selectedCategory = this.allCategories.find((x) => x.url == this.shopParams.categoryName);
      if (!this.selectedCategory) {
        this.router.navigateByUrl('/notfound');
      }
      this.getProducts();
    });
  }

  onPageChanged(event: number) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onHeaderClicked(sortText: string) {
    this.shopParams.sort = sortText;
    this.getProducts();
  }

  onRemoveFilterClick(event: ShopParams) {
    this.shopParams = structuredClone(event);
    this.getProducts();
  }

  abstract getProducts();
}
