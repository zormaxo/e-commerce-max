import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormsModule } from '@angular/forms';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { CoreRoutingModule } from './core-routing.module';
import { HasRoleDirective } from './directives/has-role.directive';
import { MainMenuComponent } from './components/main-menu/main-menu.component';

@NgModule({
  declarations: [NavBarComponent, NotFoundComponent, ServerErrorComponent, TestErrorsComponent, HasRoleDirective, MainMenuComponent],
  imports: [FormsModule, CommonModule, CoreRoutingModule, BsDropdownModule.forRoot()],
  exports: [NavBarComponent, HasRoleDirective],
})
export class CoreModule {}
