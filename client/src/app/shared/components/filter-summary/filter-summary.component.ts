import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
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
  @Output() removeFilterClicked = new EventEmitter<ShopParams>();

  searchFilter = SearchFilter;
  price: string;
  cityName: string;
  countyName: string;

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

    if (this.filterShopParams?.cityId || this.filterShopParams?.countyId) {
      this.shopService.getCities().subscribe((cities: IAddress[]) => {
        if (this.filterShopParams?.cityId) {
          this.cityName = cities.find((x) => x.id == this.filterShopParams?.cityId).name;
        }
        if (this.filterShopParams?.countyId) {
          this.countyName = cities
            .find((x) => x.id == this.filterShopParams?.cityId)
            .counties.find((x) => x.id == this.filterShopParams?.countyId).name;
        }
      });
    }
  }

  onRemoveFilterClick(event: SearchFilter) {
    const categoryName = this.filterShopParams.categoryName;

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
        this.filterShopParams.search = this.shopService.shopParams.search = '';
        break;
      case SearchFilter.city:
        this.filterShopParams.cityId = 0;
        break;
      default:
        this.filterShopParams = undefined;
        this.shopService.shopParams.search = ''; //for sync with navbar
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

    this.removeFilterClicked.emit(this.filterShopParams ?? new ShopParams(10, categoryName));
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
