import { Component, HostListener, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { MembersService } from 'src/app/core/services/members.service';
import { take } from 'rxjs';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Member } from 'src/app/shared/models/member';
import { User } from 'src/app/shared/models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-membership-info',
  templateUrl: './membership-info.component.html',
  styleUrls: ['./membership-info.component.scss'],
})
export class MembershipInfoComponent implements OnInit {
  @ViewChild('nameSurnameForm') nameSurnameForm: NgForm;
  @ViewChild('emailForm') emailForm: NgForm;
  @ViewChild('phoneForm') phoneForm: NgForm;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.nameSurnameForm.dirty || this.emailForm.dirty || this.phoneForm.dirty) {
      $event.returnValue = true;
    }
  }

  member: Member | undefined;
  user: User | null = null;
  memberClone: Member;
  uploader: FileUploader;
  hasBaseDropzoneOver = false;
  baseUrl = environment.apiUrl;
  modalRef?: BsModalRef;

  constructor(
    private memberService: MembersService,
    private accountService: AccountService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private router: Router
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user) => (this.user = user));
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  ngOnInit(): void {
    this.loadMember();
    this.initializeUploader();
  }

  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-photo-and-set-main',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (_item, response, _status, _headers) => {
      if (response) {
        const photoResp = JSON.parse(response);
        this.member.photos.push(photoResp.result);
        this.user.photoUrl = photoResp.result.url;
        this.accountService.setCurrentUser(this.user);
        this.member.photoUrl = photoResp.result.url;
        this.member.photos.forEach((p) => {
          if (p.isMain) p.isMain = false;
          if (p.id === photoResp.result.id) p.isMain = true;
        });
      }
      this.modalRef.hide();
    };
  }

  deletePhoto(photoId: number) {
    this.memberService.deletePhoto(photoId).subscribe(() => {
      this.member.photos = this.member.photos.filter((x) => x.id !== photoId);
    });
  }

  loadMember() {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.memberService.getMember(user.userId).subscribe((member: Member) => {
        this.member = member;
        this.memberClone = structuredClone(this.member);
      });
    });
  }

  updateUserFirstLastName() {
    this.memberService.updateUserFirstLastName(this.member).subscribe(() => {
      this.toastr.success('First and last name updated.');
      this.memberClone.firstName = this.member.firstName;
      this.memberClone.lastName = this.member.lastName;
      this.nameSurnameForm.reset(this.member);

      this.user.firstName = this.member.firstName;
      this.accountService.setCurrentUser(this.user);
    });
  }

  updateUsername() {
    this.memberService.updateUsername(this.member).subscribe(() => {
      this.toastr.success('Email updated.');
      this.memberClone.email = this.member.email;
      this.emailForm.reset(this.member);
      this.logout();
    });
  }

  updateUserPhone() {
    this.memberService.updateUserPhone(this.member).subscribe(() => {
      this.toastr.success('Phone number updated.');
      this.memberClone.phoneNumber = this.member.phoneNumber;
      this.phoneForm.reset(this.member);
    });
  }

  onReset(form: NgForm) {
    form.reset(this.memberClone);
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/signin');
  }
}
