import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Address, User } from 'src/app/shared/models/user';
import { PresenceService } from '../core/services/presence.service';
import { ApiResponse } from 'src/app/shared/models/api-response/api-response';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router, private presenceService: PresenceService) {}

  login(model: unknown) {
    return this.http.post<ApiResponse<User>>(this.baseUrl + 'account/login', model).pipe(
      map((response) => {
        const user = response.result;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(model: unknown) {
    return this.http.post<ApiResponse<User>>(this.baseUrl + 'account/register', model).pipe(
      map((response) => {
        const user = response.result;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;

    if (Array.isArray(roles)) {
      user.roles = roles;
    } else {
      user.roles.push(roles);
    }

    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
    this.presenceService.createHubConnection(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.presenceService.stopHubConnection();
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  checkEmailExists(email: string) {
    return this.http.get<ApiResponse<boolean>>(this.baseUrl + 'account/emailExists?email=' + email);
  }

  getUserAddress() {
    return this.http.get<ApiResponse<Address>>(this.baseUrl + 'account/address');
  }

  updateUserAddress(address: Address) {
    return this.http.put(this.baseUrl + 'account/address', address);
  }
}
