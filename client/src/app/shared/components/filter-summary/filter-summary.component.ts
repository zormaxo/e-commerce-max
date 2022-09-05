import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { ShopService } from 'src/app/_services/shop.service';
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
  @Input() cities: IAddress[];
  @Output() resetClicked = new EventEmitter<ShopParams>();

  searchFilter = SearchFilter;
  price: string;

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

  onResetClick(event: SearchFilter) {
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
      case SearchFilter.cityId:
        this.filterShopParams.cityId = 0;
        break;
      case SearchFilter.countyId:
        this.filterShopParams.countyId = 0;
        break;
      default:
        this.filterShopParams = undefined;
        this.shopService.searchTerm = '';
    }

    if (
      this.filterShopParams &&
      !this.filterShopParams.isNew &&
      !this.filterShopParams.minValue &&
      !this.filterShopParams.maxValue &&
      !this.filterShopParams.search
    ) {
      this.filterShopParams = undefined;
    }

    this.resetClicked.emit(this.filterShopParams ?? new ShopParams(10));
    console.log('ömer');
  }

  getCityName(cityId: number) {
    return this.cities.find((x) => x.id == cityId).name;
  }

  getCountyName(cityId: number, countyId: number) {
    return this.cities.find((x) => x.id == cityId).counties.find((x) => x.id == countyId).name;
  }
}

export enum SearchFilter {
  isNew,
  minValue,
  maxValue,
  search,
  price,
  cityId,
  countyId,
}
