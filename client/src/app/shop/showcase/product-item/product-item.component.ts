import { Component, Input, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss'],
})
export class ProductItemComponent implements OnInit {
  @Input() product: Product;

  imageSource: string;

  constructor() {}

  ngOnInit(): void {
    // this.imageSource = this.product.photos.find((x) => x.isMain).url;
  }
}
