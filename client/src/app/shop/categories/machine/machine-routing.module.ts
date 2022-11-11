import { NgModule } from '@angular/core';
import { MachineComponent } from './machine.component';
import { RouterModule, Routes } from '@angular/router';
import { AppProductBaseComponent } from '../app-product-base/app-product-base.component';

const routes: Routes = [
  // { path: '', component: MachineComponent },
  { path: '', redirectTo: 'makine', pathMatch: 'full' },
  { path: '', component: AppProductBaseComponent, children: [
    { path: 'makine', component: MachineComponent }] },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MachineRoutingModule {}
