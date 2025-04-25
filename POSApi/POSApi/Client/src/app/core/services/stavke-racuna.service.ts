import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateStavke_racunaDTO, GetStavke_racunaDTO } from '../../models/stavke_racuna';
import { GetProizvodDTO } from '../../models/proizvodi';

@Injectable({
  providedIn: 'root'
})
export class StavkeRacunaService {

  private baseUrl = 'https://localhost:4000';

  constructor(private http: HttpClient) {}


getAllStavke(): Observable<GetStavke_racunaDTO[]> {
  return this.http.get<GetStavke_racunaDTO[]>(`${this.baseUrl}/api/stavke_racuna`);
}

addStavkeRacuna(stavke: CreateStavke_racunaDTO): Observable<void>{
  return this.http.get<void>(`${this.baseUrl}/api/stavke_racuna`);
}


}





//' '<>[]{}