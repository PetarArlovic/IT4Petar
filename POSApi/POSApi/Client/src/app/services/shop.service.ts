import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:4000/api/';
  private http= inject (HttpClient);


  //getProducts(){
    //this.http.get<Proizvod>(this.baseUrl + 'proizvodi');

  //}
}
