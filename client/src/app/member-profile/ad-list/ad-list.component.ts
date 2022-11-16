import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from 'src/app/shop/shop.service';
import { AccountService } from 'src/app/core/services/account.service';
import { take } from 'rxjs';
import { registerLocaleData } from '@angular/common';
import localeTr from '@angular/common/locales/tr';
import { ActivatedRoute, Data, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
  activeStatus: boolean;
  page: string;

  constructor(
    private shopService: ShopService,
    private accountService: AccountService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data: Data) => {
      this.page = data['page'];
    });

    switch (this.page) {
      case 'active':
        this.activeStatus = true;
        break;
      case 'inactive':
        this.activeStatus = false;
        this.shopParams.getAllStatus = false;
        break;
      case 'favourites':
        this.shopParams.getAllStatus = true;
        this.shopParams.favourite = true;
        break;
    }

    this.getProducts();
  }

  getProducts() {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.shopParams.userId = user.userId;
      this.shopService.getProducts(this.shopParams).subscribe((response) => {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.totalCount;
      });
    });
  }

  onActive(product: IProduct, activeStatus: boolean) {
    product.isActive = activeStatus;
    this.shopService.changeActiveStatus(product).subscribe(() => {
      this.toastr.success(`${activeStatus ? 'Aktif' : 'Pasif'} edildi`);
      this.getProducts();
    });
  }
}
