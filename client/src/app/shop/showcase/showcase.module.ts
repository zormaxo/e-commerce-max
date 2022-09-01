import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowcaseComponent } from './showcase.component';
import { SharedModule } from '../../shared/shared.module';
import { OnlyNumberDirective } from '../../_directives/only-number.directive';
import { SortDirective } from '../../_directives/sort.directive';
import { ProductItemComponent } from './product-item/product-item.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [ShowcaseComponent, ProductItemComponent, SortDirective, OnlyNumberDirective],
  imports: [CommonModule, SharedModule, RouterModule],
  exports: [ShowcaseComponent, ProductItemComponent],
})
export class ShowcaseModule {}
