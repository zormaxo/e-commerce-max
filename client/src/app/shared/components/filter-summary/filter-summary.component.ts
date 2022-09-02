import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { ShopParams } from '../../models/shopParams';

@Component({
  selector: 'app-filter-summary',
  templateUrl: './filter-summary.component.html',
  styleUrls: ['./filter-summary.component.scss'],
})
export class FilterSummaryComponent implements OnChanges {
  @Input() filterShopParams: ShopParams;
  @Input() totalCount: number;
  @Output() resetClicked = new EventEmitter<ShopParams>();

  searchFilter = SearchFilter;
  price: string;

  constructor() {}

  ngOnChanges(): void {
    if (this.filterShopParams?.minValue && this.filterShopParams?.maxValue) {
      this.price = `${this.filterShopParams.minValue} - ${this.filterShopParams.maxValue} `;
    } else if (this.filterShopParams?.minValue) {
      this.price = `${this.filterShopParams.minValue} ve üzeri`;
    } else if (this.filterShopParams?.maxValue) {
      this.price = `${this.filterShopParams.minValue} ve altında`;
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
        this.filterShopParams.search = '';
        break;
      default:
        this.filterShopParams = undefined;
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
}

export enum SearchFilter {
  isNew,
  minValue,
  maxValue,
  search,
  price,
}
