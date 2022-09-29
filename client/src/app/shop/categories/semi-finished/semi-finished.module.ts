import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SemiFinishedComponent } from './semi-finished.component';
import { FormsModule } from '@angular/forms';
import { SemiFinishedRoutingModule } from './semi-finished-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [SemiFinishedComponent],
  imports: [CommonModule, FormsModule, SemiFinishedRoutingModule, SharedModule],
})
export class SemiFinishedModule {}
