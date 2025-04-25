import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ZaglavljeRacunaService {

  private baseUrl = 'https://localhost:4000';

  constructor(private http: HttpClient) { }
}
