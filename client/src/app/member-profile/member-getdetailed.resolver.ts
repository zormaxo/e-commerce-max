import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { MembersService } from './members.service';
import { Member } from '../shared/models/member';

@Injectable({
  providedIn: 'root',
})
export class MemberGetDetailResolver implements Resolve<Member> {
  constructor(private memberService: MembersService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Member> {
    return this.memberService.getMember(+route.paramMap.get('userId'));
  }
}
