import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MemberListComponent } from './member-list/member-list.component';
import { AdListComponent } from './member-profile/ad-list/ad-list.component';
import { MembershipInfoComponent } from './member-profile/membership-info/membership-info.component';
import { SummaryComponent } from './member-profile/summary/summary.component';

const routes: Routes = [
  { path: '', redirectTo: 'uyelik', pathMatch: 'full' },
  { path: 'uyelik', component: MembershipInfoComponent },
  { path: 'ozet', component: SummaryComponent },
  { path: 'ilanlar', component: AdListComponent },
  { path: 'ilanlar/pasif', component: AdListComponent },
  { path: 'omer', component: MemberListComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MemberRoutingModule {}
