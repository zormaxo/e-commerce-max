import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  users: unknown;

  uploader: FileUploader;
  hasBaseDropzoneOver = true;

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.setCurrentUser();

    this.initializeUploader();
  }

  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: 'users/add-photo',
      authToken: 'Bearer ' + 'this.user.token',
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo = JSON.parse(response);
        // this.member.photos.push(photo);
      }
    };
  }

  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e;
  }
}
