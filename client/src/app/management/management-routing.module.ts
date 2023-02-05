import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MessagesComponent } from './messages/messages.component';
import { MemberDetailComponent } from './member-detail/member-detail.component';
import { MemberDetailedResolver } from './member-detailed.resolver';
import { MemberEditComponent } from './member-edit/member-edit.component';
import { MemberListComponent } from './member-list/member-list.component';

const routes: Routes = [
  { path: '', component: MemberListComponent },
  { path: 'messages', component: MessagesComponent },
  { path: 'edit', component: MemberEditComponent },
  { path: ':userId', component: MemberDetailComponent, resolve: { member: MemberDetailedResolver } },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ManagementRoutingModule {}
