import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialComponent } from './material.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule } from '@angular/forms';
import { MaterialRoutingModule } from './material-routing.module';



@NgModule({
  declarations: [MaterialComponent],
  imports: [CommonModule, FormsModule, MaterialRoutingModule, SharedModule],
})
export class MaterialModule {}
