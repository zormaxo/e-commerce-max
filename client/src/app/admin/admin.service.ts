import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from 'src/app/shared/models/api-response/api-response';
import { User } from 'src/app/shared/models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsersWithRoles() {
    return this.http.get<ApiResponse<User[]>>(this.baseUrl + 'admin/users-with-roles');
  }

  updateUserRoles(userId: number, roles: string[]) {
    return this.http.post<ApiResponse<string[]>>(this.baseUrl + 'admin/edit-roles/' + userId + '?roles=' + roles, {});
  }
}
