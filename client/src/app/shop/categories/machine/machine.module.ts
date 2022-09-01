import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MachineComponent } from './machine.component';
import { MachineRoutingModule } from './machine-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { SortDirective } from 'src/app/_directives/sort.directive';

@NgModule({
  declarations: [MachineComponent, SortDirective],
  imports: [CommonModule, MachineRoutingModule, SharedModule],
})
export class MachineModule {}
