import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { take } from 'rxjs';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

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
      this.memberService.getMember(user.id).subscribe((member: any) => {
        this.member = member.result;
        this.memberClone = structuredClone(this.member);
      });
    });
  }

  updateMember(form: NgForm) {
    const updateClone: Member = structuredClone(this.member);

    switch (form) {
      case this.nameSurnameForm:
        updateClone.phoneNumber = null;
        updateClone.username = null;
        break;
      case this.emailForm:
        updateClone.firstName = null;
        updateClone.phoneNumber = null;
        updateClone.surname = null;
        break;
      case this.phoneForm:
        updateClone.firstName = null;
        updateClone.username = null;
        updateClone.surname = null;
        break;
      default:
        break;
    }

    this.memberService.updateMember(updateClone).subscribe(() => {
      this.toastr.success('Profile updated successfully');
      switch (form) {
        case this.nameSurnameForm:
          this.memberClone.firstName = this.member.firstName;
          this.memberClone.surname = this.member.surname;
          this.nameSurnameForm.reset(this.member);
          break;
        case this.emailForm:
          this.memberClone.username = this.member.username;
          this.emailForm.reset(this.member);
          break;
        case this.phoneForm:
          this.memberClone.phoneNumber = this.member.phoneNumber;
          this.phoneForm.reset(this.member);
          break;
        default:
          break;
      }
    });
  }

  onReset(form: NgForm) {
    form.reset(this.memberClone);
  }
}
