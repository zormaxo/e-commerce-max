import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehicleComponent } from './vehicle.component';
import { VehicleRoutingModule } from './vehicle-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [VehicleComponent],
  imports: [CommonModule, FormsModule, VehicleRoutingModule, SharedModule],
})
export class MachineModule {}
