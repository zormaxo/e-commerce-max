import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ICategory } from '../shared/models/category';
import { CategoryGroupCount } from '../shared/models/categoryGroupCount';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from '../_services/shop.service';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.scss'],
})
export class SearchResultComponent implements OnInit {
  shopParams: ShopParams = new ShopParams();
  categoryGroupCountList: CategoryGroupCount[];
  allCategories: ICategory[];
  totalCount: number;

  constructor(private shopService: ShopService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((queryParams: Params) => {
      this.shopParams.search = queryParams['search-term'];
      this.getProducts();
    });
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe((productResponse) => {
      this.categoryGroupCountList = productResponse.categoryGroupCount;
      this.totalCount = productResponse.totalCount;

      this.shopService.getCategories().subscribe((categories) => {
        this.allCategories = structuredClone(categories);

        this.categoryGroupCountList.forEach((groupCount) => {
          const category = this.allCategories.find((y) => y.id == groupCount.categoryId);
          category.count = groupCount.count;
          this.shopService.addCountToParent(category, groupCount.count);
        });
      });
    });
  }
}
