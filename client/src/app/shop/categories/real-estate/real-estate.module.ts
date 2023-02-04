import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RealEstateComponent } from './real-estate.component';
import { FormsModule } from '@angular/forms';
import { RealEstateRoutingModule } from './real-estate-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [RealEstateComponent],
  imports: [CommonModule, FormsModule, RealEstateRoutingModule, SharedModule],
})
export class RealEstateModule {}
