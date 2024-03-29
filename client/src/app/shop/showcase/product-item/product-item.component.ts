import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../../shop.service';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss'],
})
export class ProductItemComponent {
  @Input() product?: Product;

  imageSource: string;

  constructor(public shopService: ShopService, private basketService: BasketService, private toastr: ToastrService) {}

  addItemToBasket() {
    this.product && this.basketService.addItemToBasket(this.product);
    this.toastr.success('Item added to basket');
  }
}
