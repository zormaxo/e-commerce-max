import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { RouterModule } from '@angular/router';
import { CoreModule } from '../core/core.module';

@NgModule({
  declarations: [HomeComponent],
  imports: [CommonModule, RouterModule, CoreModule],
})
export class HomeModule {}
