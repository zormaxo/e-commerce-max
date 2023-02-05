import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './core/guards/auth.guard';
import { ShowcaseComponent } from './shop/showcase/showcase.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { NotFoundComponent } from './core/errors/not-found/not-found.component';
import { UserProductsComponent } from './shop/user-products/user-products.component';
import { AppProductBaseComponent } from './shop/app-product-base/app-product-base.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { AdminGuard } from './core/guards/admin.guard';

const routes: Routes = [
  { path: '', redirectTo: 'showcase', pathMatch: 'full' },
  {
    path: 'signin',
    loadChildren: () => import('./account/account.module').then((mod) => mod.AccountModule),
  },

  {
    path: '',
    component: HomeComponent,
    children: [
      { path: 'showcase', component: ShowcaseComponent },
      { path: 'product/:id', component: ProductDetailsComponent },
      { path: 'ads', redirectTo: 'showcase', pathMatch: 'full' },
      {
        path: 'ads',
        component: AppProductBaseComponent,
        children: [{ path: ':id', component: UserProductsComponent }],
      },

      {
        path: 'vehicle',
        loadChildren: () => import('./shop/categories/vehicle/vehicle.module').then((mod) => mod.MachineModule),
      },
      {
        path: 'computer',
        loadChildren: () => import('./shop/categories/computer/computer.module').then((mod) => mod.ComputerModule),
      },
      {
        path: 'real-estate',
        loadChildren: () =>
          import('./shop/categories/real-estate/real-estate.module').then((mod) => mod.RealEstateModule),
      },

      { path: 'admin', component: AdminPanelComponent, canActivate: [AdminGuard] },
      {
        path: 'search-result',
        loadChildren: () => import('./search-result/search-result.module').then((mod) => mod.SearchResultModule),
      },

      {
        path: 'member',
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

      // {
      //   path: 'ads/:id',
      //   component: UserProductsComponent,
      // },
    ],
  },

  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
