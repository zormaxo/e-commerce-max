import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowcaseComponent } from './showcase/showcase.component';
import { SharedModule } from '../shared/shared.module';
import { MachineComponent } from '../categories/machine/machine.component';
import { OnlyNumberDirective } from '../_directives/only-number.directive';
import { SortDirective } from '../_directives/sort.directive';
import { FormsModule } from '@angular/forms';
import { ProductItemComponent } from './showcase/product-item/product-item.component';
import { CoreModule } from '../core/core.module';
import { ProductDetailsComponent } from './product-details/product-details.component';

@NgModule({
  declarations: [
    ShowcaseComponent,
    ProductItemComponent,
    SortDirective,
    OnlyNumberDirective,
    ProductDetailsComponent,
  ],
  imports: [CommonModule, SharedModule, FormsModule, CoreModule],
  exports: [
    ShowcaseComponent,
    ProductItemComponent,
    ProductItemComponent,
    SortDirective,
    OnlyNumberDirective,
  ],
})
export class ShopModule {}
