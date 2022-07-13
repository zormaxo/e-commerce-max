import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShowcaseComponent } from './showcase/showcase.component';
import { SharedModule } from '../shared/shared.module';
import { MachineComponent } from './machine/machine.component';
import { OnlyNumberDirective } from '../_directives/only-number.directive';
import { SortDirective } from '../_directives/sort.directive';
import { FormsModule } from '@angular/forms';
import { ProductItemComponent } from './showcase/product-item/product-item.component';

@NgModule({
  declarations: [
    ShowcaseComponent,
    MachineComponent,
    ProductItemComponent,
    SortDirective,
    OnlyNumberDirective,
  ],
  imports: [CommonModule, SharedModule, FormsModule],
  exports: [
    ShowcaseComponent,
    ProductItemComponent,
    MachineComponent,
    ProductItemComponent,
    SortDirective,
    OnlyNumberDirective,
  ],
})
export class ShopModule {}
