import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-membership-info',
  templateUrl: './membership-info.component.html',
  styleUrls: ['./membership-info.component.scss'],
})
export class MembershipInfoComponent implements OnInit {
  member: Member;
  imageUrls = [];

  constructor(private memberService: MembersService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    this.memberService.getMember(this.route.snapshot.paramMap.get('username')).subscribe((member) => {
      this.member = member;
      this.getImages();
    });
  }

  getImages(): unknown[] {
    this.imageUrls.push({
      small: this.member.photoUrl,
      medium: this.member.photoUrl,
      big: this.member.photoUrl,
    });

    return this.imageUrls;
  }
}
