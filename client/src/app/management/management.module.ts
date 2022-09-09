import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MemberListComponent } from './member-list/member-list.component';
import { ManagementRoutingModule } from './management-routing.module';
import { FormsModule } from '@angular/forms';
import { MemberCardComponent } from './member-card/member-card.component';

@NgModule({
  declarations: [MemberListComponent, MemberCardComponent],
  imports: [CommonModule, FormsModule, ManagementRoutingModule],
})
export class ManagementModule {}
