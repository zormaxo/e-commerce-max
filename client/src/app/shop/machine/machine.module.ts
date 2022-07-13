import { NgModule } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';
import { MachineComponent } from './machine.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProductItemComponent } from './product-item/product-item.component';
import { SortDirective } from 'src/app/_directives/sort.directive';
import { FormsModule } from '@angular/forms';
import { OnlyNumberDirective } from 'src/app/_directives/only-number.directive';
import localeTr from '@angular/common/locales/tr';

registerLocaleData(localeTr);

@NgModule({
  declarations: [MachineComponent, ProductItemComponent, SortDirective, OnlyNumberDirective],
  imports: [CommonModule, SharedModule, FormsModule],
  exports: [MachineComponent, SortDirective, OnlyNumberDirective],
})
export class MachineModule {}
