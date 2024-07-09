import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
baseUrl = environment.apiUrl;
private currentUserSource = new BehaviorSubject<User | null>(null);
currentUser$ = this.currentUserSource.asObservable();

  constructor(private htpp: HttpClient, private router: Router) { }

  login(values: any){
    return this.htpp.post<User>(this.baseUrl + "account/login", values).pipe(
      map(user => {
        localStorage.setItem("token", user.token);
        this.currentUserSource.next(user);
      })
    )
  }

  register(values: any){
    return this.htpp.post<User>(this.baseUrl + "account/register",values).pipe(
      map(user =>{
        localStorage.setItem("token", user.token);
        this.currentUserSource.next(user);
      })
    )
  }

  logout(){
    localStorage.removeItem("token");
    this.currentUserSource.next(null);
    this.router.navigateByUrl("/");
  }

  checkEmailExists(email: string){
    return this.htpp.get<boolean>(this.baseUrl + "account/emailExists?email=" + email);
  }

  loadCurrentUser(token: string){
    let headers = new HttpHeaders();
    headers = headers.set("Authorization", `Bearer ${token}`);
    
    return this.htpp.get<User>(this.baseUrl + "account", {headers}).pipe(
      map(user => {
        localStorage.setItem("token", user.token);
        this.currentUserSource.next(user);
      })
    )
  }
}
