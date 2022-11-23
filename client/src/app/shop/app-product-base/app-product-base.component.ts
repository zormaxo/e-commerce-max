import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
  categoryName;
  mainCategoryName;
  allCategories: ICategory[];
  selectedCategory: ICategory;
  shopParams: ShopParams;
  mode: LeftNavMode;
  LeftNavMode = LeftNavMode;
  filteredCategories: ICategory[];
  member: Member;

  constructor(private route: ActivatedRoute, private shopService: ShopService, private router: Router) {}

  ngOnInit(): void {
    this.shopService.productAdded.subscribe((a) => {
      this.allCategories = a.allCategories;
      this.selectedCategory = a.sCategory;
      this.shopParams = a.shopParams;
      this.mainCategoryName = a.mainCategoryName;
      this.mode = a.mode;
      this.member = a.member;

      this.filteredCategories = this.allCategories.filter((x) => x.parent === undefined && x.count);
      this.shopService.fillParentCategoryList(this.selectedCategory);
    });
  }
}
