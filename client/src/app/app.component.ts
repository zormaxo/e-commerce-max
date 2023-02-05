import { Component, HostListener, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';
import { User } from './shared/models/user';
import { ShopService } from './shop/shop.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'e-commerce-max';

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.shopService.innerWidth = window.innerWidth;
  }

  constructor(
    private accountService: AccountService,
    private basketService: BasketService,
    private shopService: ShopService
  ) {}

  ngOnInit(): void {
    this.shopService.innerWidth = window.innerWidth;
    this.loadBasket();
    this.setCurrentUser();
  }

  loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) this.basketService.getBasket(basketId);
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
}
