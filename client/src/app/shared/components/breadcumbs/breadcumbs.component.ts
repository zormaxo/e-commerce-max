import { Component, Input, OnInit } from '@angular/core';
import { ShopService } from 'src/app/_services/shop.service';

@Component({
  selector: 'app-breadcumbs',
  templateUrl: './breadcumbs.component.html',
  styleUrls: ['./breadcumbs.component.scss'],
})
export class BreadcumbsComponent implements OnInit {
  @Input() selectedCategoryId: number;

  constructor(public shopService: ShopService) {}

  ngOnInit(): void {
    this.shopService.generateFilteredCategory(this.selectedCategoryId);
  }
}
