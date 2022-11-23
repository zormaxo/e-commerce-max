import { Component, Injector, OnInit } from '@angular/core';
import { AppProductBaseClass } from 'src/app/app-product-base-class';
import { LeftNavMode } from 'src/app/shared/enums/leftNavMode';

@Component({
  selector: 'app-user-products',
  templateUrl: './user-products.component.html',
  styleUrls: ['./user-products.component.scss'],
})
export class UserProductsComponent extends AppProductBaseClass {
  constructor(injector: Injector) {
    super(injector);

    this.mode = LeftNavMode.UserProducts;
  }

  override getProducts() {
    this.shopParams.userId = +this.route.snapshot.paramMap.get('id');
    return this.shopService.getProducts(this.shopParams);
  }
}
