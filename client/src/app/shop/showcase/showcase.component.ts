import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../../shared/models/product';
import { ICategory as ICategory } from '../../shared/models/category';
import { ShopParams } from '../../shared/models/shopParams';
import { ShopService } from '../shop.service';
import { CategoryGroupCount } from 'src/app/shared/models/categoryGroupCount';

@Component({
  selector: 'app-showcase',
  templateUrl: './showcase.component.html',
  styleUrls: ['./showcase.component.scss'],
})
export class ShowcaseComponent implements OnInit {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  products: IProduct[];
  categories: ICategory[];
  shopParams = new ShopParams();
  totalCount: number;
  parentCategories: ICategory[] = [];
  categoryGroupCount: CategoryGroupCount[];

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProductsThenCategories();
  }

  getProductsThenCategories() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.totalCount;
        this.categoryGroupCount = response.categoryGroupCount;

        this.getCategories();
      },
    });
  }

  getCategories() {
    this.shopService.getCategories().subscribe({
      next: (response) => {
        this.categories = structuredClone(response);

        this.categories.forEach((category) => (category.count = 0));
        this.categoryGroupCount.forEach((groupCount) => {
          const category = this.categories.find((x) => x.id == groupCount.categoryId);
          category.count = groupCount.count;
          this.shopService.addCountToParent(category, groupCount.count);
        });
      },
    });
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProductsThenCategories();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProductsThenCategories();
  }
}
