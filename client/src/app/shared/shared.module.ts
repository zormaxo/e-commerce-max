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
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { RouterModule } from '@angular/router';
import { LeftNavComponent } from './components/left-nav/left-nav.component';
import { NgxMaskModule } from 'ngx-mask';
import { FileUploadModule } from 'ng2-file-upload';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SortDirective } from '../core/directives/sort.directive';
import { OnlyNumberDirective } from '../core/directives/only-number.directive';
import { FilterComponent } from './components/filter/filter.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { DateInputComponent } from './components/date-input/date-input.component';
import { TextInputComponent } from './components/text-input/text-input.component';

@NgModule({
  declarations: [
    PagerComponent,
    PagingHeaderComponent,
    FilterSummaryComponent,
    BreadcrumbComponent,
    LeftNavComponent,
    SortDirective,
    OnlyNumberDirective,
    FilterComponent,
    DateInputComponent,
    TextInputComponent,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
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
    FileUploadModule,
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),
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
    BreadcrumbComponent,
    LeftNavComponent,
    FileUploadModule,
    ModalModule,
    SortDirective,
    FilterComponent,
    DateInputComponent,
    TextInputComponent,
  ],
})
export class SharedModule {}
