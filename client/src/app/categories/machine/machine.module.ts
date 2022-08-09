import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MachineComponent } from './machine.component';
import { CoreModule } from 'src/app/core/core.module';
import { MachineRoutingModule } from './machine-routing.module';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [MachineComponent],
  imports: [CommonModule, MachineRoutingModule, RouterModule],
})
export class MachineModule {}
