import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InputMaskModule } from 'primeng/inputmask';
import { MemberProfileRoutingModule } from './member-profile-routing.module';
import { MembershipInfoComponent } from './membership-info/membership-info.component';
import { MemberProfileComponent } from './member-profile.component';
import { SummaryComponent } from './summary/summary.component';
import { AdListComponent } from './ad-list/ad-list.component';
import { SharedModule } from '../shared/shared.module';
import { MemberProfileNavComponent } from './member-profile-nav/member-profile-nav.component';

@NgModule({
  declarations: [MemberProfileComponent, MembershipInfoComponent, SummaryComponent, AdListComponent, MemberProfileNavComponent],
  imports: [CommonModule, FormsModule, MemberProfileRoutingModule, InputMaskModule, SharedModule],
  exports: [],
})
export class MemberProfileModule {}
