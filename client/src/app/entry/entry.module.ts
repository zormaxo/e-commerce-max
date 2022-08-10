import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { EntryRoutingModule } from './entry-routing.module';
import { EntryComponent } from './entry.component';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, EntryComponent],
  imports: [CommonModule, FormsModule, EntryRoutingModule, SharedModule],
})
export class EntryModule {}
