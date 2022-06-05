import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent {
  constructor(public accountService: AccountService, private router: Router) {}

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('');
  }
}