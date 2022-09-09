import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdListComponent } from './ad-list/ad-list.component';
import { MemberProfileComponent } from './member-profile.component';
import { MembershipInfoComponent } from './membership-info/membership-info.component';
import { SummaryComponent } from './summary/summary.component';

const routes: Routes = [
  // { path: '', redirectTo: 'uyelik', pathMatch: 'full' },
  {
    path: '',
    component: MemberProfileComponent,
    children: [
      { path: 'uyelik', component: MembershipInfoComponent },
      { path: 'ozet', component: SummaryComponent },
      { path: 'ilanlar', component: AdListComponent },
      { path: 'ilanlar/pasif', component: AdListComponent },
    ],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MemberProfileRoutingModule {}
