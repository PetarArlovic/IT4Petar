import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateKupacDTO, GetKupacDTO, UpdateKupacDTO } from '../../models/kupci';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class KupciService {

  private baseUrl = environment.apiUrl

  constructor(private http: HttpClient) { }

  getAllKupci(): Observable<GetKupacDTO[]>{
    return this.http.get<GetKupacDTO[]>(`${this.baseUrl}/api/kupci`);
  }

  addKupac(kupac: CreateKupacDTO): Observable<GetKupacDTO>{
    return this.http.post<GetKupacDTO>(`${this.baseUrl}/api/kupci`, kupac)
  }

  updateKupac(sifra: number, kupac: UpdateKupacDTO): Observable<void>{
    return this.http.put<void>(`${this.baseUrl}/api/kupci/${sifra}`, kupac)
  }

  deleteKupac(sifra: number): Observable<void>{
    return this.http.delete<void>(`${this.baseUrl}/api/kupciAdmin/${sifra}`)
  }

  getKupacById(id: number): Observable<GetKupacDTO>{
    return this.http.get<GetKupacDTO>(`${this.baseUrl}/api/kupci/${id}`)
  }

  findKupacBySifra(sifra: number): Observable<GetKupacDTO>{
  return this.http.get<GetKupacDTO>(`${this.baseUrl}/api/kupci//sifra/${sifra}`)
  }
}