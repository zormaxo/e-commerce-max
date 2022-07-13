import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [NavBarComponent],
  imports: [CommonModule, BrowserAnimationsModule, BsDropdownModule.forRoot(), RouterModule],
  exports: [NavBarComponent, RouterModule],
})
export class CoreModule {}
