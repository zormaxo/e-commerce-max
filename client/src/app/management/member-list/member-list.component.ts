import { Component, OnInit } from '@angular/core';

import { MembersService } from 'src/app/member-profile/members.service';
import { Member } from 'src/app/shared/models/member';
import { Pagination2 } from 'src/app/shared/models/pagination2';
import { User } from 'src/app/shared/models/user';
import { UserParams } from 'src/app/shared/models/userParams';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss'],
})
export class MemberListComponent implements OnInit {
  members: Member[];
  pagination: Pagination2;
  userParams: UserParams;
  user: User;

  constructor(private memberService: MembersService) {
    this.userParams = this.memberService.getUserParams();
  }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.memberService.setUserParams(this.userParams);
    this.memberService.getMembers(this.userParams).subscribe((response) => {
      this.members = response.result.result;
      this.pagination = response.pagination;
    });
  }

  resetFilters() {
    this.userParams = this.memberService.resetUserParams();
    this.loadMembers();
  }

  pageChanged(event: any) {
    this.userParams.pageNumber = event.page;
    this.memberService.setUserParams(this.userParams);
    this.loadMembers();
  }
}
