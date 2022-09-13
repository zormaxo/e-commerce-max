import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth.guard';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { ShowcaseComponent } from './shop/showcase/showcase.component';
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
        canActivate: [AuthGuard],
        loadChildren: () => import('./member-profile/member-profile.module').then((mod) => mod.MemberProfileModule),
      },
      {
        path: 'uyeler',
        canActivate: [AuthGuard],
        loadChildren: () => import('./management/management.module').then((mod) => mod.ManagementModule),
      },
    ],
  },
  {
    path: 'giris',
    loadChildren: () => import('./entry/entry.module').then((mod) => mod.EntryModule),
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
