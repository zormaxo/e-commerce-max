import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.apiUrl;
  map = new Map();

  constructor(private http: HttpClient) {}

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users');
  }

  getMember(id: number) {
    return this.http.get<Member>(this.baseUrl + 'users/' + id);

    this.http.get<Member>(this.baseUrl + 'users/' + id).subscribe((_member: Member) => {
      this.map.set(id, _member);
    });
  }
}
