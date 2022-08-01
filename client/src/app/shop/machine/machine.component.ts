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
      this.getProducts();
    });
  }

  onSearchProduct() {
    this.getProducts();
  }

  getProducts() {
    this.shopService.getCategories().subscribe((response) => {
      this.selectedCategory = response.find((x) => x.url == this.categoryName);
      this.fillParentCategoryList(this.selectedCategory);
      this.fillChildCategoryIdList(this.selectedCategory);
    });

    this.shopService.getProducts(this.shopParams).subscribe(
      (response) => {
        this.products = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  fillChildCategoryIdList(childNode: ICategory) {
    this.shopParams.childCategoryIds.push(childNode.id);
    if (childNode.childCategories) {
      childNode.childCategories.forEach((child) => this.fillChildCategoryIdList(child));
    }
  }

  fillParentCategoryList(el: ICategory) {
    if (el.parent == null) {
      return;
    } else {
      this.parentCategories.unshift(el.parent);
      this.fillParentCategoryList(el.parent);
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
