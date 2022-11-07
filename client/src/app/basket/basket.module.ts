import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketComponent } from './basket.component';
import { SharedModule } from '../shared/shared.module';
import { BasketRoutingModule } from './basket-routing.module';

@NgModule({
  declarations: [BasketComponent],
  imports: [CommonModule, BasketRoutingModule, SharedModule],
})
export class BasketModule {}
