import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppProductBaseComponent } from '../../app-product-base/app-product-base.component';
import { MaterialComponent } from './material.component';

const routes: Routes = [
  { path: '', redirectTo: 'makine', pathMatch: 'full' },
  { path: '', component: AppProductBaseComponent, children: [{ path: ':subcategory', component: MaterialComponent }] },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MaterialRoutingModule {}
