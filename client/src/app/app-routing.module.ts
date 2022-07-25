import { NgModule, OnInit } from '@angular/core';
import { Routes, RouterModule, Router } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberProfileComponent } from './members/member-profile/member-profile.component';
import { MembershipInfoComponent } from './members/member-profile/membership-info/membership-info.component';
import { MemberProfileStartComponent } from './members/member-profile-start/member-profile-start.component';
import { EntryComponent } from './entry/entry.component';
import { MachineComponent } from './shop/machine/machine.component';
import { ShowcaseComponent } from './shop/showcase/showcase.component';
import { SummaryComponent } from './members/member-profile/summary/summary.component';
import { AdListComponent } from './members/member-profile/ad-list/ad-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'vitrin', pathMatch: 'full' },
  {
    path: '',
    component: HomeComponent,
    children: [
      { path: 'vitrin', component: ShowcaseComponent },
      { path: 'makine', component: MachineComponent },
      {
        path: 'uye',
        component: MemberProfileComponent,
        canActivate: [AuthGuard],
        children: [
          { path: 'uyelik', component: MembershipInfoComponent },
          { path: 'ozet', component: SummaryComponent },
          { path: 'ilanlar', component: AdListComponent },
          { path: 'ilanlar/pasif', component: AdListComponent },
        ],
      },
    ],
  },
  { path: 'giriş', component: EntryComponent },
  {
    path: '',
    canActivateChild: [AuthGuard],
    children: [
      {
        path: 'members/a/:username',
        component: MemberProfileStartComponent,
      },

      { path: 'members/a', component: MemberProfileStartComponent },
      { path: 'members/:username', component: MemberDetailComponent },
      { path: 'lists', component: ListsComponent },

      // { path: 'messages', component: MessagesComponent },
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
export class AppRoutingModule implements OnInit {
  constructor(private router: Router) {}
  ngOnInit(): void {}
}
