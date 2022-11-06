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
        this.categories = response;
        this.shopService.addCountToParents(this.categories, this.categoryGroupCount);
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
