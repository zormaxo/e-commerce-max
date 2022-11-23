import { Component } from '@angular/core';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss'],
})
export class BreadcrumbComponent {
  constructor(public shopService: ShopService) {}
}
