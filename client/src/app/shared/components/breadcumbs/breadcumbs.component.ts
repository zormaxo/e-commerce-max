import { Component, Input, OnChanges } from '@angular/core';
import { ShopService } from 'src/app/_services/shop.service';
import { ICategory } from '../../models/category';

@Component({
  selector: 'app-breadcumbs',
  templateUrl: './breadcumbs.component.html',
  styleUrls: ['./breadcumbs.component.scss'],
})
export class BreadcumbsComponent implements OnChanges {
  @Input() parentCategories: ICategory[] = [];
  @Input() selectedCategoryId: number;

  selectedCategory: ICategory;

  constructor(private shopService: ShopService) {}

  ngOnChanges(): void {
    this.shopService.getCategories().subscribe((categories) => {
      this.selectedCategory = categories.find((x) => x.id == this.selectedCategoryId);
    });
  }
}
