import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { take } from 'rxjs';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-membership-info',
  templateUrl: './membership-info.component.html',
  styleUrls: ['./membership-info.component.scss'],
})
export class MembershipInfoComponent implements OnInit {
  @ViewChild('nameSurnameForm') nameSurnameForm: NgForm;
  @ViewChild('emailForm') emailForm: NgForm;
  @ViewChild('phoneForm') phoneForm: NgForm;
  member: Member;
  memberClone: Member;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.nameSurnameForm.dirty || this.emailForm.dirty || this.phoneForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private memberService: MembersService,
    private accountService: AccountService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.memberService.getMember(user.id).subscribe((member: Member) => {
        this.member = member;
        this.memberClone = structuredClone(this.member);
      });
    });
  }

  updateUserFirstLastName() {
    this.memberService.updateUserFirstLastName(this.member).subscribe(() => {
      this.toastr.success('Profile updated successfully');
      this.memberClone.firstName = this.member.firstName;
      this.memberClone.surname = this.member.surname;
      this.nameSurnameForm.reset(this.member);

      const user: User = JSON.parse(localStorage.getItem('user'));
      user.firstName = this.member.firstName;
      this.accountService.setCurrentUser(user);
    });
  }

  updateUsername() {
    this.memberService.updateUsername(this.member).subscribe(() => {
      this.toastr.success('Profile updated successfully');
      this.memberClone.username = this.member.username;
      this.emailForm.reset(this.member);
    });
  }

  updateUserPhone() {
    this.memberService.updateUserPhone(this.member).subscribe(() => {
      this.toastr.success('Profile updated successfully');
      this.memberClone.phoneNumber = this.member.phoneNumber;
      this.phoneForm.reset(this.member);
    });
  }

  onReset(form: NgForm) {
    form.reset(this.memberClone);
  }
}
