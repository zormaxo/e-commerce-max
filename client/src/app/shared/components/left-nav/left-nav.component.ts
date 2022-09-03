import { Component, Input, OnInit } from '@angular/core';
import { ShopService } from 'src/app/_services/shop.service';
import { ICategory } from '../../models/category';

@Component({
  selector: 'app-left-nav',
  templateUrl: './left-nav.component.html',
  styleUrls: ['./left-nav.component.scss'],
})
export class LeftNavComponent implements OnInit {
  @Input() allCategories: ICategory[];
  @Input() parentCategories: ICategory[];
  @Input() selectedCategoryId: number;

  selectedCategory: ICategory;

  constructor(public shopService: ShopService) {}

  ngOnInit(): void {}
}
