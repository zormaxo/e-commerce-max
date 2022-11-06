import { Component, Input, OnChanges } from '@angular/core';
import { ShopService } from 'src/app/shop/shop.service';
import { ICategory } from '../../models/category';

@Component({
  selector: 'app-left-nav',
  templateUrl: './left-nav.component.html',
  styleUrls: ['./left-nav.component.scss'],
})
export class LeftNavComponent implements OnChanges {
  @Input() allCategories: ICategory[];
  @Input() selectedCategory: ICategory;
  parentCategories: ICategory[] = [];

  constructor(public shopService: ShopService) {}

  ngOnChanges(): void {
    // this.parentCategories = [];
    // this.fillParents(this.selectedCategory);
    this.parentCategories = this.shopService.fillParentCategoryList(this.selectedCategory);
  }

  // fillParents(selectedCategory: ICategory) {
  //   if (selectedCategory.parent) {
  //     this.parentCategories.unshift(selectedCategory.parent);
  //     this.fillParents(selectedCategory.parent);
  //   }
  // }

  // fillParentCategoryList(selectedCategory: ICategory): ICategory[] {
  //   const parentCategories = [];
  //   fillList(selectedCategory.parent);
  //   return parentCategories;

  //   function fillList(selectedCategory: ICategory) {
  //     if (selectedCategory.parent) {
  //       this.parentCategories.unshift(selectedCategory.parent);
  //       fillList(selectedCategory.parent);
  //     }
  //   }
  // }
}
