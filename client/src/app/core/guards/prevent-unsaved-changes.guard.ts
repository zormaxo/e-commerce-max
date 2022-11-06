import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MembershipInfoComponent } from '../../member-profile/membership-info/membership-info.component';

@Injectable({
  providedIn: 'root',
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(component: MembershipInfoComponent): boolean {
    if (component.nameSurnameForm.dirty || component.emailForm.dirty || component.phoneForm.dirty) {
      return confirm('Devam etmek istediğine emin misin? Kaydedilmemiş tüm değişiklikler kaybolacak');
    }
    return true;
  }
}
