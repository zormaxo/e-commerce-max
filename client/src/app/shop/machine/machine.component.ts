import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from '../../shared/models/product';
import { ICategory } from '../../shared/models/productType';
import { ShopParams } from '../../shared/models/shopParams';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-machine',
  templateUrl: './machine.component.html',
  styleUrls: ['./machine.component.scss'],
})
export class MachineComponent implements OnInit {
  @ViewChild('search', { static: true }) searchTerm: ElementRef;
  products: IProduct[];
  shopParams: ShopParams;
  totalCount: number;
  categoryName: string;
  selectedCategory: ICategory;
  parentCategories: ICategory[] = [];

  constructor(private shopService: ShopService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.params.subscribe(() => {
      this.parentCategories = [];
      this.categoryName = this.route.snapshot.url[this.route.snapshot.url.length - 1].path;
      this.shopParams = new ShopParams(10, this.categoryName);
      this.getCategoriesThenProducts();
    });
  }

  onSearchProduct() {
    this.getProducts();
  }

  getCategoriesThenProducts() {
    this.shopService.getCategories().subscribe((response) => {
      this.selectedCategory = response.find((x) => x.url == this.categoryName);
      this.fillParentCategoryList(this.selectedCategory);

      this.shopService.getProducts(this.shopParams).subscribe((productResponse) => {
        this.products = productResponse.data;
        this.shopParams.pageNumber = productResponse.pageIndex;
        this.shopParams.pageSize = productResponse.pageSize;
        this.totalCount = productResponse.count;
      });
    });
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe((productResponse) => {
      this.products = productResponse.data;
      this.shopParams.pageNumber = productResponse.pageIndex;
      this.shopParams.pageSize = productResponse.pageSize;
      this.totalCount = productResponse.count;
    });
  }

  fillParentCategoryList(selectedCategory: ICategory) {
    if (selectedCategory.parent == null) {
      return;
    } else {
      this.parentCategories.unshift(selectedCategory.parent);
      this.fillParentCategoryList(selectedCategory.parent);
    }
  }

  onPageChanged(event: number) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
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

  onHeaderClicked(sortText: string) {
    this.shopParams.sort = sortText;
    this.getProducts();
  }

  setListItemPadding(i: number) {
    return {
      'padding-left': 10 * (i + 1) + 'px',
    };
  }
}
