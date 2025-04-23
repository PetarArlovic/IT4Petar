import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetProizvodDTO } from '../../models/proizvodi';
import { Observable } from 'rxjs';
import { CreateKupacDTO, GetKupacDTO, UpdateKupacDTO } from '../../models/kupci';

@Injectable({
  providedIn: 'root'
})
export class KupciService {

  private baseUrl = 'https://localhost:4000';

  constructor(private http: HttpClient) { }


  getAllKupci(): Observable<GetKupacDTO[]>{
    return this.http.get<GetKupacDTO[]>(`${this.baseUrl}/api/kupci`);
  }

  addKupac(kupac: CreateKupacDTO): Observable<CreateKupacDTO>{
    return this.http.post<CreateKupacDTO>(`${this.baseUrl}/api/kupci`, kupac)
  }

  updateKupac(sifra: number, kupac: UpdateKupacDTO): Observable<void>{
    return this.http.put<void>(`${this.baseUrl}/api/kupci/${sifra}`, kupac)
  }

  deleteKupac(sifra: number): Observable<void>{
    return this.http.delete<void>(`${this.baseUrl}/api/kupci/${sifra}`)
  }

  getKupacById(id: number): Observable<GetKupacDTO>{
    return this.http.get<GetKupacDTO>(`${this.baseUrl}/api/kupci/${id}`)
  }

  findKupacBySifra(sifra: number): Observable<GetKupacDTO>{
  return this.http.get<GetKupacDTO>(`${this.baseUrl}/api/kupci//sifra/${sifra}`)
  }
}




//' '<>[]{}