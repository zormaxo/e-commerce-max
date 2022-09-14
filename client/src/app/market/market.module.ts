import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products/products.component';
import { MarketRoutingModule } from './market-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ProductItemComponent } from './products/product-item/product-item.component';
import { ProductDetailsComponent } from './products/product-details/product-details.component';

@NgModule({
  declarations: [ProductsComponent, ProductItemComponent, ProductDetailsComponent],
  imports: [CommonModule, MarketRoutingModule, SharedModule],
})
export class MarketModule {}
