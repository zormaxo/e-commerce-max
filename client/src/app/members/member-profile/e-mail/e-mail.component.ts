import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-e-mail',
  templateUrl: './e-mail.component.html',
  styleUrls: ['./e-mail.component.scss'],
})
export class EMailComponent implements OnInit {
  member: Member;

  constructor(public accountService: AccountService, private memberService: MembersService) {}

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user: User) => this.loadMember(user.id));
  }

  loadMember(userId: number) {
    this.member = this.memberService.fillMemberMap(userId);
  }
}
