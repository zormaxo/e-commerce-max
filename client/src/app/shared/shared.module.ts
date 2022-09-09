import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { PagerComponent } from './components/pager/pager.component';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FilterSummaryComponent } from './components/filter-summary/filter-summary.component';
import { BreadcumbsComponent } from './components/breadcumbs/breadcumbs.component';
import { RouterModule } from '@angular/router';
import { LeftNavComponent } from './components/left-nav/left-nav.component';
import { NgxMaskModule } from 'ngx-mask';

@NgModule({
  declarations: [PagerComponent, PagingHeaderComponent, FilterSummaryComponent, BreadcumbsComponent, LeftNavComponent],
  imports: [
    CommonModule,
    RouterModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
    }),
    PaginationModule.forRoot(),
    NgxGalleryModule,
    NgxMaskModule.forRoot(),
  ],
  exports: [
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    NgxGalleryModule,
    NgxMaskModule,
    FilterSummaryComponent,
    BreadcumbsComponent,
    LeftNavComponent,
  ],
})
export class SharedModule {}
