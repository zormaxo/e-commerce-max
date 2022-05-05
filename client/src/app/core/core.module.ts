import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
  declarations: [NavBarComponent],
  imports: [CommonModule, BrowserAnimationsModule, BsDropdownModule.forRoot()],
  exports: [NavBarComponent],
})
export class CoreModule {}
