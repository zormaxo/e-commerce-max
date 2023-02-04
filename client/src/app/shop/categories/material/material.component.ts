import { Component, Injector } from '@angular/core';
import { AppProductBaseClass } from 'src/app/app-product-base-class';

@Component({
  selector: 'app-material',
  templateUrl: './material.component.html',
  styleUrls: ['./material.component.scss'],
})
export class MaterialComponent extends AppProductBaseClass {
  constructor(injector: Injector) {
    super(injector);
  }

  override getProducts() {
    return this.shopService.getMaterialProducts(this.shopParams);
  }
}
