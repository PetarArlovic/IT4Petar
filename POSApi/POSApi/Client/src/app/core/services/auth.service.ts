import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../../models/auth';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  private baseUrl = 'https://localhost:4000';

  constructor(private http: HttpClient) {}


  registerUser(userDetails: User) {
    return this.http.post(`${this.baseUrl}/api/account/register`, userDetails);
  }

  loginUser(loginData: { email: string, password: string }) {
    return this.http.post<{ token: string }>(`${this.baseUrl}/api/account/login`, loginData);
  }
}

//<!--  ' '  <>  []  {}  || -->
