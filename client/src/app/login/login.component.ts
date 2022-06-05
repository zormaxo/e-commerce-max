import { Component, EventEmitter, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  @Output() cancelRegister = new EventEmitter<boolean>();
  model: { username?: string; password?: string } = {};

  constructor(public accountService: AccountService, private toastr: ToastrService) {}

  login() {
    this.accountService.login(this.model).subscribe({
      next: (response) => {
        console.log(response);
        this.cancel();
      },
      error: (error) => {
        console.log(error);
        this.toastr.error(error.error.message);
      },
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
