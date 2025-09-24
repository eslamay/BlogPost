import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest } from '../login/models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../login/models/login-response.model';
import { User } from '../login/models/user.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiBaseUrl = 'https://localhost:7177';
  private httpClient = inject(HttpClient);
  private cookieService = inject(CookieService);

  $user=new BehaviorSubject<User | undefined>(undefined);

  register(form:any){
    return this.httpClient.post(`${this.apiBaseUrl}/api/Auth/Register`, form);
  }

  login(request:LoginRequest):Observable<LoginResponse>{
    return this.httpClient.post<LoginResponse>(`${this.apiBaseUrl}/api/Auth/Login`, {
      email: request.email,
      password: request.password
    });
  }

  setUser(user:User)
  {
    this.$user.next(user);
    localStorage.setItem('user-email', user.email);
    localStorage.setItem('user-roles', user.roles.join(','));
  }

  user():Observable<User|undefined>
  {
    return this.$user.asObservable();
  }

   getUser(): User | undefined {
    const userEmail = localStorage.getItem('user-email');
    const userRoles = localStorage.getItem('user-roles');
    if (userEmail && userRoles) {
      return { email: userEmail, roles: userRoles.split(',') };
    }
    return undefined;
  }
  logout(){
     localStorage.clear();
    this.cookieService.delete('Authorization', '/');
    this.$user.next(undefined);
  }
}
