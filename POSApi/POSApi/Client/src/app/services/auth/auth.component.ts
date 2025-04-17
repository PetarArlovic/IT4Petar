import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { User } from '../../interfaces/auth';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-auth',
  imports: [],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent {

  private baseUrl = 'https://localhost:4000';

  constructor(private http: HttpClient) {}

  registerUser(userDetails: User) {
    return this.http.post(`${this.baseUrl}/api/account/register`, userDetails);
  }
////////////////////////
  loginUser(loginData: { EMAIL: string, PASSWORD: string }) {
    return this.http.post<{ token: string }>(`${this.baseUrl}/api/account/login`, loginData);
  }
}
//<!--  ' '  <>  []  {}  || -->







//<!--  ' '  <div>  []  {}  || `-->