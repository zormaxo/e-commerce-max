import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { IProduct } from '../../shared/models/product';
import { ICategory as ICategory } from '../../shared/models/productType';
import { ShopParams } from '../../shared/models/shopParams';
import { ShopService } from '../shop.service';

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

  constructor(private shopService: ShopService, private router: Router) {}

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: (response) => {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
    });
  }

  getCategories() {
    this.shopService.getCategories().subscribe({
      next: (response) => {
        this.categories = response;
      },
    });
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
