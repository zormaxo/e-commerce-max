import { Component, ElementRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from 'src/app/shop/shop.service';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  shopParams = new ShopParams();
  products: IProduct[];
  totalCount: number;

  constructor(public accountService: AccountService, private shopService: ShopService, private router: Router) {}

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('');
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.totalCount;
      },
    });
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
