import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MaterialComponent } from './material.component';

const routes: Routes = [
  { path: '', redirectTo: 'malzeme', pathMatch: 'full' },
  { path: ':subcategory', component: MaterialComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MaterialRoutingModule {}
