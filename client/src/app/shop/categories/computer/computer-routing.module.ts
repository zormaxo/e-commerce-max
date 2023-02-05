import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppProductBaseComponent } from '../../app-product-base/app-product-base.component';
import { ComputerComponent } from './computer.component';

const routes: Routes = [
  { path: '', redirectTo: 'computer', pathMatch: 'full' },
  { path: '', component: AppProductBaseComponent, children: [{ path: ':subcategory', component: ComputerComponent }] },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ComputerRoutingModule {}
