import { NgModule } from '@angular/core';
import { VehicleComponent } from './vehicle.component';
import { RouterModule, Routes } from '@angular/router';
import { AppProductBaseComponent } from '../../app-product-base/app-product-base.component';

const routes: Routes = [
  { path: '', redirectTo: 'vehicle', pathMatch: 'full' },
  { path: '', component: AppProductBaseComponent, children: [{ path: ':subcategory', component: VehicleComponent }] },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VehicleRoutingModule {}
