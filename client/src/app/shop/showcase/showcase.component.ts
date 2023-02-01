import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from '../../shared/models/product';
import { ICategory as ICategory } from '../../shared/models/category';
import { ShopParams } from '../../shared/models/shopParams';
import { ShopService } from '../shop.service';
import { CategoryGroupCount } from 'src/app/shared/models/categoryGroupCount';
import { mergeMap } from 'rxjs';

@Component({
  selector: 'app-showcase',
  templateUrl: './showcase.component.html',
  styleUrls: ['./showcase.component.scss'],
})
export class ShowcaseComponent implements OnInit {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  products: Product[] = [];
  shopParams: ShopParams;
  totalCount = 0;
  categories: ICategory[] = [];
  parentCategories: ICategory[] = [];
  categoryGroupCount: CategoryGroupCount[];

  constructor(private shopService: ShopService) {
    this.shopParams = shopService.getShopParams();
  }

  ngOnInit(): void {
    this.getProductsAndCategories();
  }

  getProductsAndCategories() {
    this.shopService
      .getProducts()
      .pipe(
        mergeMap((response) => {
          this.products = response.data;
          this.shopParams.pageNumber = response.pageIndex;
          this.shopParams.pageSize = response.pageSize;
          this.totalCount = response.totalCount;
          this.categoryGroupCount = response.categoryGroupCount;
          return this.shopService.getCategories();
        })
      )
      .subscribe({
        next: (response) => {
          this.categories = response;
          this.shopService.calculateProductCountsByCategory(this.categories, this.categoryGroupCount, true);
        },
      });
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProductsAndCategories();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProductsAndCategories();
  }
}
