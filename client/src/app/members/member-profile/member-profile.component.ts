import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-member-profile',
  templateUrl: './member-profile.component.html',
  styleUrls: ['./member-profile.component.scss'],
})
export class MemberProfileComponent implements OnInit {
  constructor(public accountService: AccountService, private memberService: MembersService) {}

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user: User) => {
      this.memberService.fillMemberMap(user.id).subscribe((member) => {
        this.memberService.map.set(member.id, member);
      });
    });
  }
}
