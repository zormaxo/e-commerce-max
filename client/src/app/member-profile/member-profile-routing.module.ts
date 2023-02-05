import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PreventUnsavedChangesGuard } from '../core/guards/prevent-unsaved-changes.guard';
import { AdListComponent } from './ad-list/ad-list.component';
import { MemberMessagesComponent } from './member-messages/member-messages.component';
import { MemberProfileNavComponent } from './member-profile-nav/member-profile-nav.component';
import { MemberProfileComponent } from './member-profile.component';
import { MembershipInfoComponent } from './membership-info/membership-info.component';
import { MessagesOutlookComponent } from './messages-outlook/messages-outlook.component';
import { SummaryComponent } from './summary/summary.component';

const routes: Routes = [
  // { path: '', redirectTo: 'membership', pathMatch: 'full' },
  {
    path: '',
    component: MemberProfileComponent,
    children: [
      { path: 'mobile', component: MemberProfileNavComponent },
      { path: 'membership', component: MembershipInfoComponent, canDeactivate: [PreventUnsavedChangesGuard] },
      { path: 'summary', component: SummaryComponent },
      { path: 'ads', component: AdListComponent, data: { page: 'active ads' } },
      { path: 'ads/inactive', component: AdListComponent, data: { page: 'inactive ads' } },
      { path: 'favorites', component: AdListComponent, data: { page: 'favorites' } },
      { path: 'messages', component: MessagesOutlookComponent },
    ],
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MemberProfileRoutingModule {}
