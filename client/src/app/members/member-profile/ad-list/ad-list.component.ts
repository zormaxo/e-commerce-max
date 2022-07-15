import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from 'src/app/shop/shop.service';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs';
import { registerLocaleData } from '@angular/common';
import localeTr from '@angular/common/locales/tr';
import { ActivatedRoute, Router } from '@angular/router';
registerLocaleData(localeTr);

@Component({
  selector: 'app-ad-list',
  templateUrl: './ad-list.component.html',
  styleUrls: ['./ad-list.component.scss'],
})
export class AdListComponent implements OnInit {
  products: IProduct[];
  shopParams = new ShopParams(10);
  totalCount: number;

  constructor(
    private shopService: ShopService,
    private accountService: AccountService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // let omer = this.route.snapshot.url[1]?.path;
    const isInactive = this.router.url.split('?')[0].split('/').pop();
    if (isInactive === 'pasif') {
      this.shopParams.getAllStatus = false;
    }
    this.getProducts();
  }

  getProducts() {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.shopParams.userId = user.id;
      this.shopService.getProducts(this.shopParams).subscribe((response) => {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      });
    });
  }
}
