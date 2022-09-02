import { NgModule } from '@angular/core';
import { MachineComponent } from './machine.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  // { path: '', component: MachineComponent },
  { path: '', redirectTo: 'makine', pathMatch: 'full' },
  { path: ':subcategory', component: MachineComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MachineRoutingModule {}
