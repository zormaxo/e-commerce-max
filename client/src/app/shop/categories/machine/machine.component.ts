import { Component, Injector } from '@angular/core';
import { IPagination } from '../../../shared/models/pagination';
import { AppProductBaseComponent } from 'src/app/app-product-base.component';

@Component({
  selector: 'app-machine',
  templateUrl: './machine.component.html',
  styleUrls: ['./machine.component.scss'],
})
export class MachineComponent extends AppProductBaseComponent {
  constructor(injector: Injector) {
    super(injector);
  }

  override getProducts() {
    this.shopService.getMachineProducts(this.shopParams).subscribe((productResponse: IPagination) => {
      this.products = productResponse.data;
      this.shopParams.pageNumber = productResponse.pageIndex;
      this.shopParams.pageSize = productResponse.pageSize;
      this.totalCount = productResponse.totalCount;

      this.shopService.addCountToParents(this.allCategories, productResponse.categoryGroupCount);
    });
  }
}
