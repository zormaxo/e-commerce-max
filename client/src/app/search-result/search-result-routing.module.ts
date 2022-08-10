import { NgModule } from '@angular/core';
import { SearchResultComponent } from './search-result.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [{ path: '', component: SearchResultComponent }];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SearchResultRoutingModule {}
