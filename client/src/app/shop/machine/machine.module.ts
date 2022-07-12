import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MachineComponent } from './machine.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductItemComponent } from './product-item/product-item.component';
import { SortDirective } from 'src/app/_directives/sort.directive';
import { OnlyNumberDirective } from 'src/app/_directives/only-number.directive';

@NgModule({
  declarations: [MachineComponent, ProductItemComponent, SortDirective, OnlyNumberDirective],
  imports: [CommonModule, SharedModule],
  exports: [MachineComponent, SortDirective, OnlyNumberDirective],
})
export class MachineModule {}
