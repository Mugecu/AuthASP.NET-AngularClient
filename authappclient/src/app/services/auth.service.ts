import { HttpClient } from '@angular/common/http';
//import { Token } from '@angular/compiler/src/ml_parser/tokens';
import { Token } from '../components/models/token';
import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { AUTH_API_URL } from '../app-injection-tokens';
import {tap} from 'rxjs/operators';

export const ACCESS_TOKEN_KEY ='bookclub_access_token'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private http: HttpClient,
    @Inject(AUTH_API_URL) private apiUrl:string,
    private jwtHelper: JwtHelperService,
    private router:Router
  ) { }
  login(email: string, password: string): Observable<Token>{
    return this.http.post<Token>(`${this.apiUrl}api/auth/login`,{
      email,password
    }).pipe(
      tap(token=> {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token)
      })
    )
  }
  isAuthenticated(): boolean{
    var token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token && !this.jwtHelper.isTokenExpired(token)
  }
  logout():void{
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['']);
  }
}
