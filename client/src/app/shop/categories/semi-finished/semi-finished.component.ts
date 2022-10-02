import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from '../../../shared/models/product';
import { ICategory } from '../../../shared/models/category';
import { ShopParams } from '../../../shared/models/shopParams';
import { ShopService } from '../../../_services/shop.service';
import { CurrencyType } from 'src/app/shared/models/currency';
import { IAddress } from 'src/app/shared/models/address';

@Component({
  selector: 'app-semi-finished',
  templateUrl: './semi-finished.component.html',
  styleUrls: ['./semi-finished.component.scss'],
})
export class SemiFinishedComponent implements OnInit {
  products: IProduct[];
  shopParams: ShopParams = new ShopParams(10);
  totalCount: number;
  categoryName: string;
  allCategories: ICategory[];
  selectedCategory: ICategory;
  cities: IAddress[];
  counties: IAddress[];

  filterShopParams: ShopParams;
  currencyType = CurrencyType;
  mainCategoryName: string;

  constructor(public shopService: ShopService, private route: ActivatedRoute, private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.shopParams.search = navigation?.extras?.state?.searchTerm;
    this.shopParams.cityId = navigation?.extras?.state?.cityId ?? 0;
    this.shopParams.countyId = navigation?.extras?.state?.countyId ?? 0;

    if (this.shopParams.search || this.shopParams.cityId || this.shopParams.countyId) {
      this.filterShopParams = new ShopParams();
      if (this.shopParams.search) this.filterShopParams.search = this.shopParams.search;
      if (this.shopParams.cityId) this.filterShopParams.cityId = this.shopParams.cityId;
      if (this.shopParams.countyId) this.filterShopParams.countyId = this.shopParams.countyId;
    }

    this.shopService.getCities().subscribe((cities: IAddress[]) => {
      this.cities = cities;
      if (this.shopParams.cityId) {
        this.counties = this.cities.find((x) => x.id == this.shopParams.cityId)?.counties;
      }
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(() => {
      this.shopParams.categoryName = this.categoryName = this.route.snapshot.url[0].path;
      this.mainCategoryName = this.route.parent.snapshot.url[0].path;
      this.getCategoriesThenProducts();
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

  getProducts() {
    this.shopService.getMaterialProducts(this.shopParams).subscribe((productResponse) => {
      this.products = productResponse.data;
      this.shopParams.pageNumber = productResponse.pageIndex;
      this.shopParams.pageSize = productResponse.pageSize;
      this.totalCount = productResponse.totalCount;

      this.shopService.addCountToParents(this.allCategories, productResponse.categoryGroupCount);
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

  // onSearch() {
  //   this.shopParams.search = this.searchTerm.nativeElement.value;
  //   this.shopParams.pageNumber = 1;
  //   if (
  //     (this.shopParams.search == '' || this.shopParams.search == undefined) &&
  //     (this.shopParams.cityId == 0 || this.shopParams.cityId == undefined) &&
  //     (this.shopParams.countyId == 0 || this.shopParams.countyId == undefined) &&
  //     this.shopParams.isNew == undefined &&
  //     (this.shopParams.maxValue == '' || this.shopParams.maxValue == undefined) &&
  //     (this.shopParams.minValue == '' || this.shopParams.minValue == undefined)
  //   ) {
  //     return;
  //   }
  //   this.filterShopParams = structuredClone(this.shopParams);
  //   this.getProducts();
  // }

  onRemoveFilterClick(event: ShopParams) {
    this.shopParams = structuredClone(event);
    this.getProducts();
  }
}
