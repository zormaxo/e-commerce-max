import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { mergeMap } from 'rxjs';
import { ICategory } from 'src/app/shared/models/category';
import { CategoryGroupCount } from 'src/app/shared/models/categoryGroupCount';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  selector: 'app-main-menu',
  templateUrl: './main-menu.component.html',
  styleUrls: ['./main-menu.component.scss'],
})
export class MainMenuComponent {
  categoryGroupCount: CategoryGroupCount[];
  categories: ICategory[] = [];

  constructor(public shopService: ShopService, public router: Router) {
    if (this.router.url.indexOf('vitrin') === -1) {
      this.getProductsAndCategories();
    }
  }

  getProductsAndCategories() {
    this.shopService
      .getProducts()
      .pipe(
        mergeMap((response) => {
          this.categoryGroupCount = response.categoryGroupCount;
          return this.shopService.getCategories();
        })
      )
      .subscribe({
        next: (response) => {
          this.categories = response;
          this.shopService.calculateProductCountsByCategory(this.categories, this.categoryGroupCount, true);
        },
      });
  }
}
