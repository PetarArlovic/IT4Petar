import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ObservedValueOf } from 'rxjs';
import { CreateStavke_racunaDTO, GetStavke_racunaDTO, UpdateStavke_racunaDTO } from '../../models/stavke_racuna';
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

addStavkeRacuna(stavke: CreateStavke_racunaDTO): Observable<GetStavke_racunaDTO>{
  return this.http.post<GetStavke_racunaDTO>(`${this.baseUrl}/api/stavke_racuna`, stavke);
}

updateStavkeRacuna(broj: number, stavke: UpdateStavke_racunaDTO): Observable<void>{
  return this.http.put<void>(`${this.baseUrl}/api/stavke_racuna/${broj}`, stavke);
}

deleteStavkeRacuna(broj: number): Observable<void>{
  return this.http.delete<void>(`${this.baseUrl}/api/stavke_racuna/${broj}`);
}

GetStavkeRacunaById(id: number): Observable<GetStavke_racunaDTO>{
  return this.http.get<GetStavke_racunaDTO>(`${this.baseUrl}/api/stavke_racuna/${id}`);
}

GetStavkeRacunaByBroj(broj: number): Observable<GetStavke_racunaDTO>{
  return this.http.get<GetStavke_racunaDTO>(`${this.baseUrl}/api/stavke_racuna/BROJ/${broj}`);
}
}





//' '<>[]{}