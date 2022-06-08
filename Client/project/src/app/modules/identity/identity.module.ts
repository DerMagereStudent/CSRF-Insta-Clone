import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CookieModule } from 'ngx-cookie';
import { IdentityComponent } from './identity.component';
import { SharedModule } from '../shared/shared.module';
import { IdentityRoutingModule } from './identity-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { LoginComponent } from './components/login/login.component';

@NgModule({
  imports: [
    CommonModule,
    CookieModule.withOptions(),
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    IdentityRoutingModule
  ],
  declarations: [
    IdentityComponent,
    SignUpComponent,
    LoginComponent
  ]
})
export class IdentityModule { }
