import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [NavBarComponent],
  imports: [FormsModule, CommonModule, RouterModule, BsDropdownModule.forRoot()],
  exports: [NavBarComponent],
})
export class CoreModule {}
