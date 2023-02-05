import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map, take } from 'rxjs/operators';
import { of } from 'rxjs';
import { AccountService } from '../account/account.service';
import { Member } from 'src/app/shared/models/member';
import { User } from 'src/app/shared/models/user';
import { UserParams } from 'src/app/shared/models/userParams';
import { getPaginationHeaders, getPaginatedResult } from '../core/services/paginationHelper';
import { ApiResponse } from '../shared/models/api-response/api-response';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  memberCache = new Map();
  user: User | undefined;
  userParams: UserParams | undefined;

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) {
          this.userParams = new UserParams(user);
          this.user = user;
        }
      },
    });
  }

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  resetUserParams() {
    if (this.user) {
      this.userParams = new UserParams(this.user);
      return this.userParams;
    }
    return;
  }

  getMembers(userParams: UserParams) {
    const response = this.memberCache.get(Object.values(userParams).join('-'));
    if (response) {
      return of(response);
    }

    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge.toString());
    params = params.append('maxAge', userParams.maxAge.toString());
    params = params.append('orderBy', userParams.orderBy);

    return getPaginatedResult<Member[]>(this.baseUrl + 'users', params, this.http).pipe(
      map((response) => {
        this.memberCache.set(Object.values(userParams).join('-'), response);
        return response;
      })
    );
  }

  getMember(userId: number) {
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: Member) => member.id === userId);

    if (member) return of(member);

    return this.http.get<ApiResponse<Member>>(this.baseUrl + 'users/' + userId).pipe(
      map((response) => {
        return response.result;
      })
    );
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users/update-member', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = { ...this.members[index], ...member };
      })
    );
  }

  updateUserFirstLastName(member: Member) {
    return this.http.put(this.baseUrl + 'users/update-user-first-last-name', member);
  }

  updateUsername(member: Member) {
    return this.http.put(this.baseUrl + 'users/update-username', member);
  }

  updateUserPhone(member: Member) {
    return this.http.put(this.baseUrl + 'users/update-user-phone', member);
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }
}
