import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  @Output() cancelRegister = new EventEmitter<boolean>();
  model: { username?: string; password?: string } = {};

  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router) {}

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('');
      },
      error: (error) => {
        console.log(error);
        this.toastr.error(error.message);
      },
    });
  }
}
