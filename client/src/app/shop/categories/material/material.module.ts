import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialComponent } from './material.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { SortDirective } from 'src/app/_directives/sort.directive';
import { OnlyNumberDirective } from 'src/app/_directives/only-number.directive';
import { FormsModule } from '@angular/forms';
import { MaterialRoutingModule } from './material-routing.module';



@NgModule({
  declarations: [MaterialComponent],
  imports: [CommonModule, FormsModule, MaterialRoutingModule, SharedModule],
})
export class MaterialModule {}
