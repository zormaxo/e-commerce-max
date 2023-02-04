import { Component, Injector } from '@angular/core';
import { AppProductBaseClass } from 'src/app/app-product-base-class';

@Component({
  selector: 'app-computer',
  templateUrl: './computer.component.html',
  styleUrls: ['./computer.component.scss'],
})
export class ComputerComponent extends AppProductBaseClass {
  constructor(injector: Injector) {
    super(injector);
  }

  override getProducts() {
    return this.shopService.getMaterialProducts(this.shopParams);
  }
}
