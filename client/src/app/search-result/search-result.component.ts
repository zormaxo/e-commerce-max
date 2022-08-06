import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ICategory } from '../shared/models/category';
import { CategoryGroupCount } from '../shared/models/categoryGroupCount';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from '../shop/shop.service';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.scss'],
})
export class SearchResultComponent implements OnInit {
  shopParams: ShopParams = new ShopParams();
  categoryGroupCount: CategoryGroupCount[];
  allCategories: ICategory[];
  searchTerm: string;
  constructor(private shopService: ShopService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((queryParams: Params) => {
      this.shopParams.search = queryParams['search-term'];
    });

    this.getProducts();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe((productResponse) => {
      this.categoryGroupCount = productResponse.categoryGroupCount;

      this.shopService.getCategories().subscribe((categories) => {
        this.allCategories = categories;

        this.categoryGroupCount.forEach((groupCount) => {
          const category = this.allCategories.find((y) => y.id == groupCount.categoryId);
          category.count = groupCount.count;
          this.addCountToParent(category);
        });
      });
    });
  }

  addCountToParent(selectedCategory: ICategory) {
    if (selectedCategory.parent) {
      selectedCategory.parent.count += selectedCategory.count;
      this.addCountToParent(selectedCategory.parent);
    }
  }
}
