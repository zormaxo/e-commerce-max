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
import { ShowcaseComponent } from './shop/showcase/showcase.component';
import { SummaryComponent } from './members/member-profile/summary/summary.component';
import { AdListComponent } from './members/member-profile/ad-list/ad-list.component';
import { SearchResultComponent } from './search-result/search-result.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';

const routes: Routes = [
  { path: '', redirectTo: 'vitrin', pathMatch: 'full' },
  {
    path: '',
    component: HomeComponent,
    children: [
      { path: 'vitrin', component: ShowcaseComponent },
      { path: 'search-result', component: SearchResultComponent },
      {
        path: 'makine',
        loadChildren: () => import('./categories/machine/machine.module').then((mod) => mod.MachineModule),
      },
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
  { path: 'product/:id', component: ProductDetailsComponent },
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
