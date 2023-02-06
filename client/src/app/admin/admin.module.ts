import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PhotoManagementComponent } from './photo-management/photo-management.component';
import { UserManagementComponent } from './user-management/user-management.component';
import { RolesModalComponent } from '../modals/roles-modal/roles-modal.component';
import { FormsModule } from '@angular/forms';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { AdminRoutingModule } from './admin-routing.module';

@NgModule({
  declarations: [PhotoManagementComponent, UserManagementComponent, RolesModalComponent, AdminPanelComponent],
  imports: [CommonModule, FormsModule, SharedModule, CoreModule, AdminRoutingModule],
  exports: [PhotoManagementComponent, UserManagementComponent, RolesModalComponent, AdminPanelComponent],
})
export class AdminModule {}
