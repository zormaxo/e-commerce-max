import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MachineComponent } from './machine.component';
import { MachineRoutingModule } from './machine-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [MachineComponent],
  imports: [CommonModule, FormsModule, MachineRoutingModule, SharedModule],
})
export class MachineModule {}
