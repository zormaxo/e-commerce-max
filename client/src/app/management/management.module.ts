import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MemberListComponent } from './member-list/member-list.component';
import { ManagementRoutingModule } from './management-routing.module';
import { MemberCardComponent } from './member-card/member-card.component';
import { MemberDetailComponent } from './member-detail/member-detail.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [MemberListComponent, MemberCardComponent, MemberDetailComponent],
  imports: [CommonModule, SharedModule, ManagementRoutingModule],
})
export class ManagementModule {}
