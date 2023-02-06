import { Component, OnInit } from '@angular/core';
import { LeftNavMode } from 'src/app/shared/enums/leftNavMode';
import { Member } from 'src/app/shared/models/member';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from '../shop.service';
import { ICategory } from 'd:/Codes/Home/Kuyumdan/client/src/app/shared/models/category';

@Component({
  selector: 'app-app-product-base',
  templateUrl: './app-product-base.component.html',
  styleUrls: ['./app-product-base.component.scss'],
})
export class AppProductBaseComponent implements OnInit {
  categoryName: string;
  mainCategoryName: string;
  allCategories: ICategory[];
  selectedCategory: ICategory;
  shopParams: ShopParams;
  mode: LeftNavMode;
  LeftNavMode = LeftNavMode;
  filteredCategories: ICategory[];
  member: Member;

  constructor(public shopService: ShopService) {}

  ngOnInit(): void {
    this.shopService.productsFetched.subscribe((props) => {
      this.allCategories = props.allCategories;
      this.selectedCategory = props.selectedCategory;
      this.shopParams = props.shopParams;
      this.mainCategoryName = props.mainCategoryName;
      this.mode = props.leftNavMode;
      this.member = props.member;

      this.filteredCategories = this.allCategories.filter((x) => x.parent === undefined && x.count);
      this.shopService.fillParentCategoryList(this.selectedCategory);
    });
  }
}
