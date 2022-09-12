import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from 'src/app/_services/shop.service';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs';
import { registerLocaleData } from '@angular/common';
import localeTr from '@angular/common/locales/tr';
import { ActivatedRoute, Router } from '@angular/router';
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
  inActivePage;

  constructor(
    private shopService: ShopService,
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    const isInactive = this.router.url.split('?')[0].split('/').pop();
    this.inActivePage = isInactive === 'pasif' ? false : true;
    if (!this.inActivePage) {
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
        this.totalCount = response.totalCount;
      });
    });
  }

  onActive(product: IProduct, activeStatus: boolean) {
    product.isActive = activeStatus;
    this.shopService.updateProduct(product).subscribe(() => {
      this.toastr.success(`${activeStatus ? 'Aktif' : 'Pasif'} edildi`);
      this.getProducts();
    });
  }
}
