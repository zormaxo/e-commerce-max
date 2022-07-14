import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from 'src/app/shop/shop.service';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss'],
})
export class SummaryComponent implements OnInit {
  products: IProduct[];
  shopParams = new ShopParams(10);
  totalCount: number;
  activeCount: number;
  inactiveCount: number;

  constructor(private shopService: ShopService, private accountService: AccountService) {}

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.getProductsWithUserId(user.id);
    });
  }

  getProductsWithUserId(userId: number) {
    this.shopParams.userId = userId;
    this.shopParams.getAllStatus = true;
    this.shopService.getProducts(this.shopParams).subscribe((response) => {
      this.products = response.data;
      this.totalCount = response.count;
      this.activeCount = this.products.filter((x) => x.isActive).length;
      this.inactiveCount = this.totalCount - this.activeCount;
    });
  }
}
