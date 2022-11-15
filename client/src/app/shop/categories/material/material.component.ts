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

  getProducts() {
    this.shopService.getMaterialProducts(this.shopParams).subscribe((productResponse) => {
      this.products = productResponse.data;
      this.shopParams.pageNumber = productResponse.pageIndex;
      this.shopParams.pageSize = productResponse.pageSize;
      this.totalCount = productResponse.totalCount;

      this.shopService.addCountToParents(this.allCategories, productResponse.categoryGroupCount);
    });
  }
}
