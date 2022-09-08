import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberProfileComponent } from './members/member-profile/member-profile.component';
import { MembershipInfoComponent } from './members/member-profile/membership-info/membership-info.component';
import { ShowcaseComponent } from './shop/showcase/showcase.component';
import { SummaryComponent } from './members/member-profile/summary/summary.component';
import { AdListComponent } from './members/member-profile/ad-list/ad-list.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';

const routes: Routes = [
  { path: '', redirectTo: 'vitrin', pathMatch: 'full' },
  {
    path: '',
    component: HomeComponent,
    children: [
      { path: 'vitrin', component: ShowcaseComponent },
      { path: 'product/:id', component: ProductDetailsComponent },
      {
        path: 'search-result',
        loadChildren: () => import('./search-result/search-result.module').then((mod) => mod.SearchResultModule),
      },
      {
        path: 'makine',
        loadChildren: () => import('./shop/categories/machine/machine.module').then((mod) => mod.MachineModule),
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
  {
    path: 'giris',
    loadChildren: () => import('./entry/entry.module').then((mod) => mod.EntryModule),
  },
  {
    path: '',
    canActivateChild: [AuthGuard],
    children: [
      { path: 'members/:username', component: MemberDetailComponent },
      { path: 'lists', component: ListsComponent },
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
export class AppRoutingModule {}
