import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppProductBaseComponent } from '../../app-product-base/app-product-base.component';
import { SemiFinishedComponent } from './semi-finished.component';

const routes: Routes = [
  { path: '', redirectTo: 'real-estate', pathMatch: 'full' },
  {
    path: '',
    component: AppProductBaseComponent,
    children: [{ path: ':subcategory', component: SemiFinishedComponent }],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SemiFinishedRoutingModule {}
