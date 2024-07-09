import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { NavBarComponent } from '../nav-bar/nav-bar.component';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService, private router: Router, private navbar: NavBarComponent){}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(auth => {
        if(auth) return true;
        else {
          this.router.navigate(["/account/login"], {queryParams: {returnUrl: state.url}});
          
          return false
        }
      })
    );
  }
}
