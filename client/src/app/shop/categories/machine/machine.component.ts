import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from '../../../shared/models/product';
import { ICategory } from '../../../shared/models/category';
import { ShopParams } from '../../../shared/models/shopParams';
import { IPagination } from '../../../shared/models/pagination';
import { ShopService } from '../../../_services/shop.service';
import { CategoryProductCount } from 'src/app/shared/models/categoryGroupCount';
import { CurrencyType } from 'src/app/shared/models/currency';
import { IAddress } from 'src/app/shared/models/address';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-machine',
  templateUrl: './machine.component.html',
  styleUrls: ['./machine.component.scss'],
})
export class MachineComponent implements OnInit, AfterViewInit {
  @ViewChild('search', { static: false }) searchTerm: ElementRef;
  products: IProduct[];
  shopParams: ShopParams = new ShopParams(10);
  totalCount: number;
  categoryGroupCount: CategoryProductCount[];
  categoryName: string;
  allCategories: ICategory[];
  selectedCategory: ICategory;
  cities: IAddress[];
  counties: IAddress[];

  filterShopParams: ShopParams;
  currencyType = CurrencyType;
  imageSource: string;

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

    this.shopService.searchClicked.subscribe((shopParams: ShopParams) => {
      this.shopParams = shopParams;
      this.filterShopParams = structuredClone(this.shopParams);
      this.getProducts();
    });
  }

  ngAfterViewInit(): void {
    this.searchTerm.nativeElement.value = this.shopParams.search != undefined ? this.shopParams.search : '';
  }

  ngOnInit(): void {
    this.route.params.subscribe(() => {
      this.shopParams.categoryName = this.categoryName =
        this.route.snapshot.url[this.route.snapshot.url.length - 1].path;
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
    this.shopService.getMachineProducts(this.shopParams).subscribe((productResponse: IPagination) => {
      this.products = productResponse.data;
      this.shopParams.pageNumber = productResponse.pageIndex;
      this.shopParams.pageSize = productResponse.pageSize;
      this.totalCount = productResponse.totalCount;
      this.categoryGroupCount = productResponse.categoryGroupCount;

      this.allCategories.forEach((category) => (category.count = 0));
      this.categoryGroupCount.forEach((groupCount) => {
        const category = this.allCategories.find((x) => x.id == groupCount.categoryId);
        category.count = groupCount.count;
        this.shopService.addCountToParents(category, groupCount.count);
      });
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

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    if (
      (this.shopParams.search == '' || this.shopParams.search == undefined) &&
      (this.shopParams.cityId == 0 || this.shopParams.cityId == undefined) &&
      (this.shopParams.countyId == 0 || this.shopParams.countyId == undefined) &&
      this.shopParams.isNew == undefined &&
      (this.shopParams.maxValue == '' || this.shopParams.maxValue == undefined) &&
      (this.shopParams.minValue == '' || this.shopParams.minValue == undefined)
    ) {
      return;
    }
    this.filterShopParams = structuredClone(this.shopParams);
    this.getProducts();
  }

  onResetClicked(event: ShopParams) {
    this.shopParams = structuredClone(event);
    this.searchTerm.nativeElement.value = this.shopParams.search != undefined ? this.shopParams.search : '';
    this.shopParams.categoryName = this.categoryName;
    this.getProducts();
  }

  onSelectChange(selectedValue: number) {
    this.counties = this.cities.find((x) => x.id == selectedValue)?.counties;
  }
}
