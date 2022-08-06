import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from '../../shared/models/product';
import { ICategory } from '../../shared/models/category';
import { ShopParams } from '../../shared/models/shopParams';
import { ShopService } from '../shop.service';
import { CategoryGroupCount } from 'src/app/shared/models/categoryGroupCount';

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
  categoryGroupCount: CategoryGroupCount[];
  categoryName: string;
  selectedCategory: ICategory;
  parentCategories: ICategory[];

  map = new Map();

  constructor(private shopService: ShopService, private route: ActivatedRoute, private router: Router) {}

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
    this.shopService.getCategories().subscribe((categories) => {
      this.selectedCategory = categories.find((x) => x.url == this.categoryName);
      if (!this.selectedCategory) {
        this.router.navigateByUrl('/notfound');
      }
      this.fillParentCategoryList(this.selectedCategory);
      this.getProducts();
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

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe((productResponse) => {
      this.products = productResponse.data;
      this.shopParams.pageNumber = productResponse.pageIndex;
      this.shopParams.pageSize = productResponse.pageSize;
      this.totalCount = productResponse.totalCount;
      this.categoryGroupCount = productResponse.categoryGroupCount;
    });
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

  fillChildCategoryIdList(category: ICategory, childCategories: ICategory[]) {
    if (category.childCategories) {
      category.childCategories.forEach((child) => this.fillChildCategoryIdList(child, childCategories));
    } else {
      childCategories.push(category);
    }
  }

  countChildProducts(category: ICategory) {
    const childCategories: ICategory[] = [];
    this.fillChildCategoryIdList(category, childCategories);
    let totalCount = 0;
    childCategories.forEach((x) => {
      const categoryToBeSummed = this.categoryGroupCount.find((o) => o.categoryId === x.id);
      if (categoryToBeSummed) {
        totalCount += categoryToBeSummed.count;
      }
    });

    return totalCount;
  }

  omer() {
    this.shopParams.search = 'purple';
    this.shopParams.categoryName = undefined;

    this.shopService.getProducts(this.shopParams).subscribe((productResponse) => {
      this.products = productResponse.data;
      this.shopParams.pageNumber = productResponse.pageIndex;
      this.shopParams.pageSize = productResponse.pageSize;
      this.totalCount = productResponse.totalCount;
      this.categoryGroupCount = productResponse.categoryGroupCount;
      

      this.shopService.getCategories().subscribe((categories) => {
        let category = categories.find((x) => x.url == this.categoryName);
        if (!this.selectedCategory) {
          this.router.navigateByUrl('/notfound');
        }
        this.fillParentCategoryList(this.selectedCategory);
        this.getProducts();
      });
    });

    //  this.map.set(id, _member);
  }
}
