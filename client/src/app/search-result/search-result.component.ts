import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { mergeMap } from 'rxjs';
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
  categoryGroupCountList: CategoryGroupCount[];
  allCategories: ICategory[];
  totalCount: number;
  searcText: string;

  constructor(public shopService: ShopService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.shopService.resetShopParams();

    this.route.queryParams.subscribe((queryParams: Params) => {
      this.searcText = queryParams['search-term'];
      this.shopService.shopParams.search = queryParams['search-term'];
      this.getProducts();
    });
  }

  getProducts() {
  this.shopService
      .getCategories()
      .pipe(
        mergeMap((categories) => {
          this.allCategories = categories;
          return this.shopService.getProducts();
        })
      )
      .subscribe((productResponse) => {
        this.totalCount = productResponse.totalCount;
        this.categoryGroupCountList = productResponse.categoryGroupCount;
        this.shopService.calculateProductCountsByCategory(this.allCategories, this.categoryGroupCountList);
      });
  }
}
