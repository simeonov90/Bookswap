import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl:string = "https://localhost:7213/api/Account";
  constructor(private http : HttpClient) { }

  signUp(registerDto:any){
    return this.http.post<any>(`${this.baseUrl}/Register`, registerDto);
  }

  login(loginObj:any){
    return this.http.post<any>(`${this.baseUrl}/Login`, loginObj);
  }
}
