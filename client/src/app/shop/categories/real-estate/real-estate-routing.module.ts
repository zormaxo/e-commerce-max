import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppProductBaseComponent } from '../../app-product-base/app-product-base.component';
import { RealEstateComponent } from './real-estate.component';

const routes: Routes = [
  { path: '', redirectTo: 'real-estate', pathMatch: 'full' },
  {
    path: '',
    component: AppProductBaseComponent,
    children: [{ path: ':subcategory', component: RealEstateComponent }],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RealEstateRoutingModule {}
