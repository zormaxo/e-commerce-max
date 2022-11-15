import { Component, Injector } from '@angular/core';
import { IPagination } from '../../../shared/models/pagination';
import { AppProductBaseClass } from 'src/app/app-product-base-class';
import { IProduct } from 'src/app/shared/models/product';

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
    // this.shopService.getMachineProducts(this.shopParams).subscribe((productResponse: IPagination<IProduct[]>) => {
    //   this.products = productResponse.data;
    //   this.shopParams.pageNumber = productResponse.pageIndex;
    //   this.shopParams.pageSize = productResponse.pageSize;
    //   this.totalCount = productResponse.totalCount;

    //   this.shopService.addCountToParents(this.allCategories, productResponse.categoryGroupCount);
    //   // this.shopService.productAdded.next({ allCategories: this.allCategories, sCategory: this.selectedCategory });
    //   this.shopService.productAdded2.next(3);
    // });

    return this.shopService.getMachineProducts(this.shopParams);
  }
}
