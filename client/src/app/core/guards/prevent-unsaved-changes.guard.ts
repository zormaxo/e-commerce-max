import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable, of } from 'rxjs';
import { MembershipInfoComponent } from '../../member-profile/membership-info/membership-info.component';
import { ConfirmService } from '../services/confirm.service';

@Injectable({
  providedIn: 'root',
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  constructor(private confirmService: ConfirmService) {}

  canDeactivate(component: MembershipInfoComponent): Observable<boolean> {
    if (component.nameSurnameForm.dirty || component.emailForm.dirty || component.phoneForm.dirty) {
      return this.confirmService.confirm();
    }
    return of(true);
  }
}
