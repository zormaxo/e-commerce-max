import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-member-profile-nav',
  templateUrl: './member-profile-nav.component.html',
  styleUrls: ['./member-profile-nav.component.scss'],
})
export class MemberProfileNavComponent {
  constructor(public accountService: AccountService) {}
}
