import { Component} from '@angular/core';
import { NavBarComponent } from 'src/app/core/nav-bar/nav-bar.component';
import { AccountService } from '../account.service';
import { AbstractControl, AsyncValidatorFn, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { debounceTime, finalize, map, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: "register.component.html",
  styleUrls: ["register.component.scss"]
})
export class RegisterComponent {
  complexPassword = "(?=^.{8,20}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$";
  errors: string [] | null = null;

  constructor(private navBarComponent: NavBarComponent, private fb: FormBuilder, private accountService: AccountService, private router: Router){}
  
  closeModal(){
    this.navBarComponent.closeModal();
  }

  openLogin() {
   this.navBarComponent.openLoginModal();
  }

  registerForm = this.fb.group({
    displayName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email], [this.validateEmailCheck()]],
    password: ['',[Validators.required,Validators.pattern(this.complexPassword)]]
  })

  onSubmit(){
    this.accountService.register(this.registerForm.value).subscribe({
      next: () => this.router.navigateByUrl("/shop"),
      error: error => this.errors = error.errors
    })
  }

  validateEmailCheck(): AsyncValidatorFn{
    return (control: AbstractControl) => {
      return control.valueChanges.pipe(
        debounceTime(1000),
        take(1),
        switchMap(() => {
          return this.accountService.checkEmailExists(control.value).pipe(
            map(result => result ? {emailExists: true} : null),
            finalize ( () => control.markAsTouched())
          )
        })
      )
    }
  }
}
