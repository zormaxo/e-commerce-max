import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  @Output() cancelRegister = new EventEmitter<boolean>();
  model: { username?: string; password?: string } = {};

  constructor(public accountService: AccountService, private toastr: ToastrService, private router: Router) {}

  login() {
    this.accountService.login(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('');
      },
    });
  }
}
