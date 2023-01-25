import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountRoutingModule } from './account-routing.module';
import { AccountComponent } from './account.component';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, AccountComponent],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, AccountRoutingModule, SharedModule],
})
export class AccountModule {}
