import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { map } from 'rxjs';
import { ShopService } from 'src/app/shop/shop.service';
import { IAddress } from '../../models/address';
import { CurrencyType } from '../../models/currency';
import { ShopParams } from '../../models/shopParams';

@Component({
  selector: 'app-filter-summary',
  templateUrl: './filter-summary.component.html',
  styleUrls: ['./filter-summary.component.scss'],
})
export class FilterSummaryComponent implements OnChanges {
  @Input() filterShopParams: ShopParams;
  @Input() totalCount: number;
  @Output() removeFilterClick = new EventEmitter<ShopParams>();

  searchFilter = SearchFilter;
  price: string;
  categoryName: string;

  constructor(private shopService: ShopService) {}

  ngOnChanges(): void {
    if (this.filterShopParams?.minValue && this.filterShopParams?.maxValue) {
      this.price = `${this.filterShopParams.minValue} ${CurrencyType[this.filterShopParams.currency]} - 
      ${this.filterShopParams.maxValue} ${CurrencyType[this.filterShopParams.currency]}`;
    } else if (this.filterShopParams?.minValue) {
      this.price = `${this.filterShopParams.minValue} ${CurrencyType[this.filterShopParams.currency]} ve üzeri`;
    } else if (this.filterShopParams?.maxValue) {
      this.price = `${this.filterShopParams.maxValue} ${CurrencyType[this.filterShopParams.currency]} ve altında`;
    }
  }

  onRemoveFilterClick(event: SearchFilter) {
    this.categoryName = this.filterShopParams.categoryName;

    switch (event) {
      case SearchFilter.isNew:
        this.filterShopParams.isNew = undefined;
        break;
      case SearchFilter.price:
        this.filterShopParams.minValue = undefined;
        this.filterShopParams.maxValue = undefined;
        this.price = undefined;
        break;
      case SearchFilter.search:
        this.filterShopParams.search = this.shopService.searchTerm = '';
        break;
      case SearchFilter.city:
        this.filterShopParams.cityId = 0;
        break;
      default:
        this.filterShopParams = undefined;
        this.shopService.searchTerm = ''; //for sync with navbar
    }

    if (
      this.filterShopParams &&
      this.filterShopParams.isNew == undefined &&
      !this.filterShopParams.minValue &&
      !this.filterShopParams.maxValue &&
      !this.filterShopParams.search
    ) {
      this.filterShopParams = undefined;
    }

    this.removeFilterClick.emit(this.filterShopParams ?? new ShopParams(10, this.categoryName));
  }

  getCityName(cityId: number) {
    return this.shopService.getCities().pipe(
      map((cities: IAddress[]) => {
        return cities.find((x) => x.id == cityId).name;
      })
    );
  }

  getCountyName(cityId: number, countyId: number) {
    return this.shopService.getCities().pipe(
      map((cities: IAddress[]) => {
        return cities.find((x) => x.id == cityId).counties.find((x) => x.id == countyId).name;
      })
    );
  }
}

export enum SearchFilter {
  isNew,
  minValue,
  maxValue,
  search,
  price,
  city,
  county,
}
