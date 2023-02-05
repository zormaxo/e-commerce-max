import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/shared/models/product';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from 'src/app/shop/shop.service';
import { AccountService } from 'src/app/account/account.service';
import { CategoryGroupCount } from 'src/app/shared/models/categoryGroupCount';
import { ICategory } from 'src/app/shared/models/category';
import { BasketItem } from 'src/app/shared/models/basket';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  products: Product[];
  totalCount: number;

  categoryGroupCount: CategoryGroupCount[];
  categories: ICategory[] = [];

  constructor(
    public accountService: AccountService,
    public shopService: ShopService,
    public basketService: BasketService,
    private router: Router
  ) {}

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('');
  }

  onSearch(event?: Event) {
    let searchTerm = undefined;
    if (event) {
      const input = event.target as HTMLInputElement;
      searchTerm = input.value;
    } else {
      searchTerm = this.shopService.shopParams.search;
    }

    if (searchTerm) {
      this.router.navigate(['search-result'], {
        queryParams: { 'search-term': searchTerm },
      });
    } else {
      this.shopService.shopParams.search = '';
    }
  }

  getCount(items: BasketItem[]) {
    return items.reduce((sum, item) => sum + item.quantity, 0);
  }
}

// (keyup.enter)="onSearch()" removed from code but can be used later.
