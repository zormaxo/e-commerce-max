import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-member-profile',
  templateUrl: './member-profile.component.html',
  styleUrls: ['./member-profile.component.scss'],
})
export class MemberProfileComponent {
  constructor(public accountService: AccountService) {}
}
