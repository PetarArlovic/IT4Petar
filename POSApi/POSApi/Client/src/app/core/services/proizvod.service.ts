import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateProizvodDTO, GetProizvodDTO, UpdateProizvodDTO } from '../../models/proizvodi';

@Injectable({
  providedIn: 'root'
})
export class ProizvodiService {

  private baseUrl = 'https://localhost:4000';

  constructor(private http: HttpClient) {}


  getAllProizvodi(): Observable<GetProizvodDTO[]> {
    return this.http.get<GetProizvodDTO[]>(`${this.baseUrl}/api/proizvodi`);
  }

  addProizvod(proizvod: CreateProizvodDTO): Observable<CreateProizvodDTO> {
    return this.http.post<CreateProizvodDTO>(`${this.baseUrl}/api/proizvodiAdmin`, proizvod);
  }

  updateProizvod(sifra: number, proizvod: UpdateProizvodDTO): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/api/proizvodi/${sifra}`, proizvod);
  }

  deleteProizvod(sifra: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/proizvodiAdmin/${sifra}`);
  }

  getProizvodById(id: number): Observable<GetProizvodDTO>{
    return this.http.get<GetProizvodDTO>(`${this.baseUrl}/api/proizvodiAdmin/${id}`)
  }

  findProizvodBySifra(sifra: number): Observable<GetProizvodDTO>{
    return this.http.get<GetProizvodDTO>(`${this.baseUrl}/api/proizvodi/sifra/${sifra}`);
  }

  findProizvodByNaziv(naziv: string):Observable<GetProizvodDTO>{
    return this.http.get<GetProizvodDTO>(`${this.baseUrl}/api/proizvodi/naziv/${naziv}`)
  }
}