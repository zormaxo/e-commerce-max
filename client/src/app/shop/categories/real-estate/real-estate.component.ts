import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../../../shared/models/product';
import { ICategory } from '../../../shared/models/category';
import { ShopParams } from '../../../shared/models/shopParams';
import { ShopService } from '../../shop.service';
import { CurrencyType } from 'src/app/shared/models/currency';
import { IAddress } from 'src/app/shared/models/address';
import { AppProductBaseClass } from 'src/app/app-product-base-class';

@Component({
  selector: 'app-real-estate',
  templateUrl: './real-estate.component.html',
  styleUrls: ['./real-estate.component.scss'],
})
export class RealEstateComponent extends AppProductBaseClass {
  constructor(injector: Injector) {
    super(injector);
  }

  override getProducts() {
    return this.shopService.getSemiFinishedProducts(this.shopParams);
  }
}
