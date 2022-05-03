import { Component } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent {
  isLoggedIn = false;
  model: unknown = {};

  constructor(public accountService: AccountService) {}

  login() {
    this.accountService.login(this.model).subscribe({
      next(response) {
        console.log(response);
      },
      error(error) {
        console.log(error);
      },
    });
  }

  logout() {
    this.accountService.logout();
  }
}
