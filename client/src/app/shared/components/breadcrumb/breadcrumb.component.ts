import { Component, Input, OnChanges } from '@angular/core';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss'],
})
export class BreadcrumbComponent implements OnChanges {
  @Input() selectedCategoryId: number;

  constructor(public shopService: ShopService) {}

  ngOnChanges(): void {
    this.shopService.generateBreadcrumb(this.selectedCategoryId);
  }
}
