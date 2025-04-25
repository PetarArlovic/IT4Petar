import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateZaglavlje_racunaDTO, GetZaglavlje_racunaDTO, UpdateZaglavlje_racunaDTO } from '../../models/zaglavlje_racuna';

@Injectable({
  providedIn: 'root'
})
export class ZaglavljeRacunaService {

  private baseUrl = 'https://localhost:4000';

  constructor(private http: HttpClient) {}

  getAllZaglavlja():Observable<GetZaglavlje_racunaDTO[]>{
    return this.http.get<GetZaglavlje_racunaDTO[]>(`${this.baseUrl}/api/zaglavlje_racuna`);
  }

  addZaglavljeRacuna(zaglavlje: CreateZaglavlje_racunaDTO): Observable<CreateZaglavlje_racunaDTO>{
    return this.http.post<CreateZaglavlje_racunaDTO>(`${this.baseUrl}/api/zaglavlje_racuna`, zaglavlje);
  }

  updateZaglavljeRacuna(broj: number, zaglavlje: UpdateZaglavlje_racunaDTO): Observable<void>{
    return this.http.put<void>(`${this.baseUrl}/api/zaglavlje_racuna/${broj}`, zaglavlje);
  }

  deleteZaglavljeRacuna(broj: number): Observable<void>{
    return this.http.delete<void>(`${this.baseUrl}/api/zaglavlje_racuna/${broj}`);
  }

  getZaglavljeRacunaById(id: number): Observable<GetZaglavlje_racunaDTO>{
    return this.http.get<GetZaglavlje_racunaDTO>(`${this.baseUrl}/api/zaglavlje_racuna/${id}`);
  }

  getZaglavljeRacunaByBroj(broj: number): Observable<GetZaglavlje_racunaDTO>{
    return this.http.get<GetZaglavlje_racunaDTO>(`${this.baseUrl}/api/zaglavlje_racuna/broj/${broj}`);
  }

}


//' '<>[]{}