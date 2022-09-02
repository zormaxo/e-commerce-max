import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowcaseComponent } from './showcase.component';
import { SharedModule } from '../../shared/shared.module';
import { ProductItemComponent } from './product-item/product-item.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [ShowcaseComponent, ProductItemComponent],
  imports: [CommonModule, SharedModule, RouterModule],
  exports: [ShowcaseComponent, ProductItemComponent],
})
export class ShowcaseModule {}
