import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MemberCardComponent } from './member-card/member-card.component';
import { MemberProfileComponent } from './member-profile/member-profile.component';
import { MembershipInfoComponent } from './member-profile/membership-info/membership-info.component';
import { MemberDetailComponent } from './member-detail/member-detail.component';
import { MemberListComponent } from './member-list/member-list.component';
import { FormsModule } from '@angular/forms';
import { SummaryComponent } from './member-profile/summary/summary.component';
import { AdListComponent } from './member-profile/ad-list/ad-list.component';
import { RouterModule } from '@angular/router';
import { InputMaskModule } from 'primeng/inputmask';

@NgModule({
  declarations: [
    MemberCardComponent,
    MemberProfileComponent,
    MembershipInfoComponent,
    MemberDetailComponent,
    MemberListComponent,
    SummaryComponent,
    AdListComponent,
  ],
  imports: [CommonModule, FormsModule, RouterModule, InputMaskModule],
  exports: [
    MemberCardComponent,
    MemberProfileComponent,
    MembershipInfoComponent,
    MemberDetailComponent,
    MemberListComponent,
  ],
})
export class MemberModule {}
