import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from 'src/app/_services/shop.service';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  shopParams = new ShopParams();
  products: IProduct[];
  totalCount: number;

  constructor(public accountService: AccountService, public shopService: ShopService, private router: Router) {}

  ngOnInit(): void {
    this.shopParams.search = this.shopService.searchTerm;
  }

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
    // const searchTerm = this.searchTerm.nativeElement.value;
    const searchTerm = this.shopParams.search;
    this.router.navigate(['search-result'], { queryParams: { 'search-term': searchTerm } });
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
