import { Component, Input, OnChanges } from '@angular/core';
import { ShopService } from 'src/app/_services/shop.service';
import { IProduct } from '../../models/product';

@Component({
  selector: 'app-left-nav',
  templateUrl: './left-nav.component.html',
  styleUrls: ['./left-nav.component.scss'],
})
export class LeftNavComponent implements OnChanges {
  @Input() selectedCategoryId: number;
  @Input() products: IProduct[];

  constructor(public shopService: ShopService) {}

  ngOnChanges(): void {
    this.shopService.generateCustomCategory(this.selectedCategoryId);
  }
}
