import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComputerComponent } from './computer.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule } from '@angular/forms';
import { ComputerRoutingModule } from './computer-routing.module';

@NgModule({
  declarations: [ComputerComponent],
  imports: [CommonModule, FormsModule, ComputerRoutingModule, SharedModule],
})
export class ComputerModule {}
