import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MachineComponent } from './machine.component';
import { MachineRoutingModule } from './machine-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { SortDirective } from 'src/app/_directives/sort.directive';
import { OnlyNumberDirective } from 'src/app/_directives/only-number.directive';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [MachineComponent, SortDirective, OnlyNumberDirective],
  imports: [CommonModule, FormsModule, MachineRoutingModule, SharedModule],
})
export class MachineModule {}
