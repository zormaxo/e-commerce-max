import { Component, Injector } from '@angular/core';
import { AppProductBaseClass } from 'src/app/app-product-base-class';

@Component({
  selector: 'app-machine',
  templateUrl: './machine.component.html',
  styleUrls: ['./machine.component.scss'],
})
export class MachineComponent extends AppProductBaseClass {
  constructor(injector: Injector) {
    super(injector);
  }

  override getProducts() {
    return this.shopService.getMachineProducts(this.shopParams);
  }
}
