import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-membership-info',
  templateUrl: './membership-info.component.html',
  styleUrls: ['./membership-info.component.scss'],
})
export class MembershipInfoComponent implements OnInit {
  member: Member;
  val4: string = '5523409795';

  constructor(private memberService: MembersService, private accountService: AccountService) {}

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.memberService.getMember(user.id).subscribe((member: Member) => {
        this.member = member;
      });
    });
  }
}
