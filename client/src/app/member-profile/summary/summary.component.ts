import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from 'src/app/shop/shop.service';
import { AccountService } from 'src/app/core/services/account.service';
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
  counts: Counts;

  constructor(private shopService: ShopService, private accountService: AccountService) {}

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.shopService.getProductCounts(user.userId).subscribe((counts: any) => {
        this.counts = counts.result;
      });
    });
  }
}

interface Counts {
  activeProducts: number;
  inactiveProducts: number;
}
