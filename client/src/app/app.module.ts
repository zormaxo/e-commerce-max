import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CoreModule } from './core/core.module';
import { FormsModule } from '@angular/forms';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { SharedModule } from './shared/shared.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { ShowcaseModule } from './shop/showcase/showcase.module';
import { HomeModule } from './home/home.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { SearchResultModule } from './search-result/search-result.module';
import { ManagementModule } from './management/management.module';
import { MemberProfileModule } from './member-profile/member-profile.module';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { MarketModule } from './market/market.module';
import { FileUploadModule } from 'ng2-file-upload';

@NgModule({
  declarations: [
    AppComponent,
    ListsComponent,
    MessagesComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    ProductDetailsComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CoreModule,
    SharedModule,
    ShowcaseModule,
    MemberProfileModule,
    SearchResultModule,
    HomeModule,
    ManagementModule,
    NgxSpinnerModule,
    MarketModule,
    FileUploadModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}
