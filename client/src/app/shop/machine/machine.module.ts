import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MachineComponent } from './machine.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductItemComponent } from './product-item/product-item.component';
import { SortDirective } from 'src/app/_directives/sort.directive';

@NgModule({
  declarations: [MachineComponent, ProductItemComponent, SortDirective],
  imports: [CommonModule, SharedModule],
  exports: [MachineComponent, SortDirective],
})
export class MachineModule {}
