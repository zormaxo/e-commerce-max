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
    if (event == SearchFilter.isNew) {
      this.filterShopParams.isNew = undefined;
    } else {
      this.filterShopParams = new ShopParams(10);
    }
    this.resetClicked.emit(this.filterShopParams);
  }
}

export enum SearchFilter {
  isNew = 'asc',
  Desc = 'desc',
  None = '',
}
