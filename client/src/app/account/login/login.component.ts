import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NavBarComponent } from 'src/app/core/nav-bar/nav-bar.component';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl:"login.component.html",
  styleUrls: ["login.component.scss"]
})
export class LoginComponent {
loginForm = new FormGroup({
  email: new FormControl("", [Validators.required, Validators.email]),
  password: new FormControl("", Validators.required)
})
returnUrl: string;

  constructor(private navBarComponent: NavBarComponent, private accountService: AccountService, private router: Router, 
    private activatedRoute: ActivatedRoute){
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop'
  }

  closeModal(){
    this.navBarComponent.closeModal();
  }
  openSignUpModal(){
    this.navBarComponent.openSignUpModal();
  }

   onSubmit(){
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => {
        this.closeModal();
        this.router.navigateByUrl('/shop');
      }   
    })
    
  }
}
