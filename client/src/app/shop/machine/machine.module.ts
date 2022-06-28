import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MachineComponent } from './machine.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductItemComponent } from './product-item/product-item.component';

@NgModule({
  declarations: [MachineComponent, ProductItemComponent],
  imports: [CommonModule, SharedModule],
  exports: [MachineComponent],
})
export class MachineModule {}
