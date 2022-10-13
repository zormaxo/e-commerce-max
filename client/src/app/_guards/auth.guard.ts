import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../core/services/account.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(private accountService: AccountService, private toastr: ToastrService, private router: Router) {}
  canActivateChild(): Observable<boolean> {
    return this.canActivate();
  }

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map((user) => {
        if (user) return true;

        this.router.navigateByUrl('/');
        this.toastr.error('You shall not pass!');
      })
    );
  }
}
