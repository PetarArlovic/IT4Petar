import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { GetProizvodDTO } from '../../models/proizvodi';

@Injectable({
  providedIn: 'root'
})

export class ShopService {
  baseUrl = 'https://localhost:4000/api/';
  private http= inject (HttpClient);

  getProizvodi(){
    return this.http.get<GetProizvodDTO[]>(this.baseUrl);
  }
  constructor() { }
}
