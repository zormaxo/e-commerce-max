import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberProfileComponent } from './members/member-profile/member-profile.component';
import { MembershipInfoComponent } from './members/member-profile/membership-info/membership-info.component';
import { MemberProfileStartComponent } from './members/member-profile-start/member-profile-start.component';
import { EntryComponent } from './entry/entry.component';
import { MachineComponent } from './shop/machine/machine.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'entry', component: EntryComponent },
  {
    path: '',
    canActivateChild: [AuthGuard],
    children: [
      {
        path: 'members/a/:username',
        component: MemberProfileStartComponent,
      },
      {
        path: 'members',
        component: MemberProfileComponent,
        canActivate: [AuthGuard],
        children: [{ path: 'a/:username/info', component: MembershipInfoComponent }],
      },
      { path: 'members/a', component: MemberProfileStartComponent },
      { path: 'members/:username', component: MemberDetailComponent },
      { path: 'lists', component: ListsComponent },

      { path: 'messages', component: MessagesComponent },
    ],
  },
  { path: 'makine', component: MachineComponent },
  { path: 'errors', component: TestErrorsComponent },
  { path: 'notfound', component: NotFoundComponent },
  { path: 'servererror', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
