import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/member';
import { map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) {}

  getMembers() {
    if (this.members.length > 0) return of(this.members);
    return this.http.get(this.baseUrl + 'users').pipe(
      map((response: any) => {
        this.members = response.result;
        return response.result;
      })
    );
  }

  getMember(userId: number) {
    const member = this.members.find((x) => x.id === userId);
    if (member !== undefined) return of(member);
    return this.http.get(this.baseUrl + 'users/' + userId).pipe(
      map((response: any) => {
        return response.result;
      })
    );
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }
}
