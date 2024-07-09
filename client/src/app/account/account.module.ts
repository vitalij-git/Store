import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { LoginBrowserComponent } from './login-browser/login-browser.component';



@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    LoginBrowserComponent

  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule,

  ],
  exports:[
    AccountRoutingModule,
    LoginComponent,
    RegisterComponent,
    LoginBrowserComponent
  ]
})
export class AccountModule { }
