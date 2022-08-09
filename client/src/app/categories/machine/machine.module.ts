import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MachineComponent } from './machine.component';
import { MachineRoutingModule } from './machine-routing.module';
import { CoreModule } from 'src/app/core/core.module';

@NgModule({
  declarations: [MachineComponent],
  imports: [CommonModule, MachineRoutingModule, CoreModule],
})
export class MachineModule {}
