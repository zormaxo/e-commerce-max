import { Component, Input, OnChanges } from '@angular/core';
import { ShopService } from 'src/app/_services/shop.service';
import { ICategory } from '../../models/category';
import { IProduct } from '../../models/product';

@Component({
  selector: 'app-left-nav',
  templateUrl: './left-nav.component.html',
  styleUrls: ['./left-nav.component.scss'],
})
export class LeftNavComponent implements OnChanges {
  @Input() selectedCategoryId: number;
  @Input() products: IProduct[];

  @Input() allCategories: ICategory[];
  @Input() selectedCategory: ICategory;
  parentCategories: ICategory[] = [];

  constructor(public shopService: ShopService) {}

  ngOnChanges(): void {
    this.shopService.generateFilteredCategory(this.selectedCategoryId);
  }

  fillList(selectedCategory: ICategory) {
    if (selectedCategory.parent) {
      this.parentCategories.unshift(selectedCategory.parent);
      this.fillList(selectedCategory.parent);
    }
  }
}
