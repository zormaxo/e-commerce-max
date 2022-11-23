import { Component, Input } from '@angular/core';
import { ShopService } from 'src/app/shop/shop.service';
import { LeftNavMode as LeftNavMode } from '../../enums/leftNavMode';
import { ICategory } from '../../models/category';

@Component({
  selector: 'app-left-nav',
  templateUrl: './left-nav.component.html',
  styleUrls: ['./left-nav.component.scss'],
})
export class LeftNavComponent {
  @Input() filteredCategories: ICategory[];
  @Input() leftNavMode: LeftNavMode;
  LeftNavMode = LeftNavMode;

  constructor(public shopService: ShopService) {}

  selectCategory(category: ICategory) {
    this.shopService.categorySelected.next(category);
  }
}
