import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IProduct } from '../../../shared/models/product';
import { ICategory } from '../../../shared/models/category';
import { ShopParams } from '../../../shared/models/shopParams';
import { ShopService } from '../../../_services/shop.service';
import { CategoryGroupCount } from 'src/app/shared/models/categoryGroupCount';

@Component({
  selector: 'app-machine',
  templateUrl: './machine.component.html',
  styleUrls: ['./machine.component.scss'],
})
export class MachineComponent implements OnInit, AfterViewInit {
  @ViewChild('search', { static: false }) searchTerm: ElementRef;
  products: IProduct[];
  shopParams: ShopParams = new ShopParams(10);
  totalCount: number;
  categoryGroupCount: CategoryGroupCount[];
  categoryName: string;
  parentCategories: ICategory[];
  allCategories: ICategory[];
  selectedCategory: ICategory;

  filterShopParams: ShopParams;

  constructor(private shopService: ShopService, private route: ActivatedRoute, private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    this.shopParams.search = navigation?.extras?.state?.searchTerm;
  }
  ngAfterViewInit(): void {
    this.searchTerm.nativeElement.value = this.shopParams.search != undefined ? this.shopParams.search : '';
  }

  ngOnInit(): void {
    this.route.params.subscribe(() => {
      this.parentCategories = [];
      this.shopParams.categoryName = this.categoryName =
        this.route.snapshot.url[this.route.snapshot.url.length - 1].path;
      this.getCategoriesThenProducts();
    });
  }

  getCategoriesThenProducts() {
    this.shopService.getCategories().subscribe((categories) => {
      this.allCategories = structuredClone(categories);
      this.selectedCategory = this.allCategories.find((x) => x.url == this.shopParams.categoryName);
      if (!this.selectedCategory) {
        this.router.navigateByUrl('/notfound');
      }
      this.fillParentCategoryList(this.selectedCategory);
      this.getProducts();
    });
  }

  fillParentCategoryList(selectedCategory: ICategory) {
    if (selectedCategory.parent) {
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

      this.allCategories.forEach((category) => (category.count = 0));
      this.categoryGroupCount.forEach((groupCount) => {
        const category = this.allCategories.find((x) => x.id == groupCount.categoryId);
        category.count = groupCount.count;
        this.shopService.addCountToParent(category, groupCount.count);
      });
    });
  }

  onPageChanged(event: number) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  onHeaderClicked(sortText: string) {
    this.shopParams.sort = sortText;
    this.getProducts();
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.filterShopParams = structuredClone(this.shopParams);
    this.getProducts();
  }

  onResetClicked(event: ShopParams) {
    this.shopParams = structuredClone(event);
    this.searchTerm.nativeElement.value = this.shopParams.search != undefined ? this.shopParams.search : '';
    this.shopParams.categoryName = this.categoryName;
    this.getProducts();
  }
}
