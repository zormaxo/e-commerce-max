import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SemiFinishedComponent } from './semi-finished.component';

const routes: Routes = [
  { path: '', redirectTo: 'yari-mamul', pathMatch: 'full' },
  { path: ':subcategory', component: SemiFinishedComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SemiFinishedRoutingModule {}
