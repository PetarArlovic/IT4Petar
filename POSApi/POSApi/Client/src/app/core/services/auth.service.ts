import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../../models/auth';
import { Observable, pipe, tap } from 'rxjs';
import { jwtDecode } from 'jwt-decode';


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
    return this.http.post<{ token: string }>(`${this.baseUrl}/api/account/login`, loginData)
  }

  getRoleFromToken(): string | null {
    const token = sessionStorage.getItem('token');
    if (token) {
      try {
        const decodedToken: any = jwtDecode(token);
        return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;
      } catch (error) {
        console.error('Greška pri dekodiranju tokena', error);
        return null;
      }
    }
    return null;
  }

  isAdmin(): boolean {
    const userRole = this.getRoleFromToken();
    return userRole === 'Admin';
  }
}

//<!--  ' '  <>  []  {}  || -->
