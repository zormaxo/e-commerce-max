import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { ShowcaseComponent } from './shop/showcase/showcase.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { NotFoundComponent } from './core/errors/not-found/not-found.component';

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
        path: 'malzeme',
        loadChildren: () => import('./shop/categories/material/material.module').then((mod) => mod.MaterialModule),
      },
      {
        path: 'yari-mamul',
        loadChildren: () =>
          import('./shop/categories/semi-finished/semi-finished.module').then((mod) => mod.SemiFinishedModule),
      },
      {
        path: 'uye',
        canActivate: [AuthGuard],
        loadChildren: () => import('./member-profile/member-profile.module').then((mod) => mod.MemberProfileModule),
      },
      {
        path: 'uyeler',
        canActivate: [AuthGuard],
        loadChildren: () => import('./management/management.module').then((mod) => mod.ManagementModule),
      },
      {
        path: 'market',
        canActivate: [AuthGuard],
        loadChildren: () => import('./market/market.module').then((mod) => mod.MarketModule),
      },
      {
        path: 'error',
        canActivate: [AuthGuard],
        loadChildren: () => import('./core/core.module').then((mod) => mod.CoreModule),
      },
    ],
  },
  {
    path: 'giris',
    loadChildren: () => import('./entry/entry.module').then((mod) => mod.EntryModule),
  },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
