import { Component } from '@angular/core';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-browser',
  templateUrl:"login-browser.component.html",
  styleUrls: ["login-browser.component.scss"]
})
export class LoginBrowserComponent {
  loginForm = new FormGroup({
    email: new FormControl("", [Validators.required, Validators.email]),
    password: new FormControl("", Validators.required)
  })
  returnUrl: string;

  constructor(private accountService: AccountService, private router: Router, 
    private activatedRoute: ActivatedRoute){
    this.returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '/shop'
  }

  onSubmit(){
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => {
        this.router.navigateByUrl('/shop');
      }   
    })
  }
}
