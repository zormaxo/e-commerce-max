import { Component, Input, OnChanges } from '@angular/core';
import { ShopService } from 'src/app/_services/shop.service';

@Component({
  selector: 'app-breadcumbs',
  templateUrl: './breadcumbs.component.html',
  styleUrls: ['./breadcumbs.component.scss'],
})
export class BreadcumbsComponent implements OnChanges {
  @Input() selectedCategoryId: number;

  constructor(public shopService: ShopService) {}

  ngOnChanges(): void {
    this.shopService.generateFilteredCategory(this.selectedCategoryId);
  }
}
