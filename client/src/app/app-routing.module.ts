import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './core/guards/auth.guard';
import { ShowcaseComponent } from './shop/showcase/showcase.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { NotFoundComponent } from './core/errors/not-found/not-found.component';
import { UserProductsComponent } from './shop/user-products/user-products.component';
import { AppProductBaseComponent } from './shop/app-product-base/app-product-base.component';

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
        path: 'shop',
        canActivate: [AuthGuard],
        loadChildren: () => import('./s-project/shop/shop.module').then((mod) => mod.ShopModule),
      },
      {
        path: 'basket',
        canActivate: [AuthGuard],
        loadChildren: () => import('./basket/basket.module').then((mod) => mod.BasketModule),
      },
      {
        path: 'checkout',
        loadChildren: () => import('./checkout/checkout.module').then((mod) => mod.CheckoutModule),
        data: { breadcrumb: 'Checkout' },
      },
      {
        path: 'error',
        canActivate: [AuthGuard],
        loadChildren: () => import('./core/core.module').then((mod) => mod.CoreModule),
      },
      {
        path: 'urunler',
        component: AppProductBaseComponent,
        children: [{ path: ':id', component: UserProductsComponent }],
      },
      // {
      //   path: 'urunler/:id',
      //   component: UserProductsComponent,
      // },
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
