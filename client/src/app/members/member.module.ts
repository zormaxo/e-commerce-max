import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MemberCardComponent } from './member-card/member-card.component';
import { MemberProfileStartComponent } from './member-profile-start/member-profile-start.component';
import { MemberProfileComponent } from './member-profile/member-profile.component';
import { MembershipInfoComponent } from './member-profile/membership-info/membership-info.component';
import { MemberDetailComponent } from './member-detail/member-detail.component';
import { MemberListComponent } from './member-list/member-list.component';
import { SharedModule } from '../shared/shared.module';
import { CoreModule } from '../core/core.module';
import { FormsModule } from '@angular/forms';
import { SummaryComponent } from './member-profile/summary/summary.component';
import { AdListComponent } from './member-profile/ad-list/ad-list.component';

@NgModule({
  declarations: [
    MemberCardComponent,
    MemberProfileComponent,
    MembershipInfoComponent,
    MemberProfileStartComponent,
    MemberDetailComponent,
    MemberListComponent,
    SummaryComponent,
    AdListComponent,
  ],
  imports: [CommonModule, SharedModule, CoreModule, FormsModule],
  exports: [
    MemberCardComponent,
    MemberProfileComponent,
    MembershipInfoComponent,
    MemberProfileStartComponent,
    MemberDetailComponent,
    MemberListComponent,
  ],
})
export class MemberModule {}
