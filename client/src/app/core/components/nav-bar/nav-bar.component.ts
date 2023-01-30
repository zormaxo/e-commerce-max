import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/shared/models/product';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from 'src/app/shop/shop.service';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  shopParams = new ShopParams();
  products: Product[];
  totalCount: number;

  constructor(public accountService: AccountService, public shopService: ShopService, private router: Router) {}

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('');
  }

  onSearch() {
    this.router.navigate(['search-result'], { queryParams: { 'search-term': this.shopService.searchTerm } });
  }

  onReset() {
    this.shopService.searchTerm = '';
  }
}
