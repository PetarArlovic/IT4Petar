import { Component, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ProizvodiService } from '../../core/services/proizvod.service';
import { CartProizvodDTO, GetProizvodDTO } from '../../models/proizvodi';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { CardModule } from 'primeng/card';
import { CommonModule } from '@angular/common';
import { ShopRegisterComponent } from '../shop-register/shop-register.component';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { debounceTime, distinctUntilChanged } from 'rxjs';

@Component({
  selector: 'app-home',
  imports: [ButtonModule, CardModule, CommonModule, ShopRegisterComponent, TableModule, PaginatorModule, ReactiveFormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})

export class HomeComponent implements OnInit {

  proizvodi: GetProizvodDTO[] = [];
  kosarica: CartProizvodDTO[] = [];
  totalCost: number = 0;
  searchControl = new FormControl;
  rezultat: GetProizvodDTO[] = [];

  constructor (
    private proizvodiService: ProizvodiService,
    private messageService: MessageService
  ) {}

    ngOnInit(): void {
      this.loadProizvodi();

      //Search bar.
      this.searchControl.valueChanges
        .pipe(debounceTime(300), distinctUntilChanged())
        .subscribe((input: string) => {
          if (!input || input.trim() === '') {
            this.loadProizvodi();
            return;
          }

          const nazivProizvoda = input.trim();
          const sifra = Number(nazivProizvoda);

          if (!isNaN(sifra)) {
            this.proizvodiService.findProizvodBySifra(sifra).subscribe({
              next: (proizvod) => {
                this.proizvodi = [{
                  ...proizvod,
                  proizvodSlikaUrl: `/images/${proizvod.proizvodSlikaUrl}`
                }];
              },
              error: () => {
                this.proizvodi = [];
                this.messageService.add({
                  severity: 'error',
                  summary: 'Greška',
                  detail: `Nema proizvoda sa šifrom: ${sifra}`,
                  life: 3000
                });
              }
            });

          } else {
            this.proizvodiService.findProizvodByNaziv(nazivProizvoda).subscribe({
              next: (proizvod) => {
                this.proizvodi = [{
                  ...proizvod,
                  proizvodSlikaUrl: `/images/${proizvod.proizvodSlikaUrl}`
                }];
              },
              error: () => {
                this.proizvodi = [];
                this.messageService.add({
                  severity: 'error',
                  summary: 'Greška',
                  detail: `Nema proizvoda s nazivom: "${nazivProizvoda}"`,
                  life: 3000
                });
              }
          });
        }
    });
  }


  loadProizvodi(): void {
    this.proizvodiService.getAllProizvodi().subscribe(proizvodi => {

      this.proizvodi = proizvodi.map(p => ({
        ...p,
        proizvodSlikaUrl: `/images/${p.proizvodSlikaUrl}`
      }));
    });
  }

  addToCart(proizvod: GetProizvodDTO) {
    const existing = this.kosarica.find(p => p.sifra === proizvod.sifra);

    if (existing) {
      existing.kolicina++;
      existing.vrijednost = (existing.cijena - (existing.cijena * (existing.popust / 100))) * existing.kolicina;
      this.kosarica = [...this.kosarica];
    } else {
      const novaStavka: CartProizvodDTO = {
        ...proizvod,
        kolicina: 1,
        vrijednost: (proizvod.cijena - (proizvod.cijena * (proizvod.popust / 100))),
        popust: proizvod.popust || 0,
      };
      this.kosarica = [...this.kosarica, novaStavka];
    }

    this.recalculateTotal();
  }

  private recalculateTotal() {
    this.totalCost = this.kosarica
      .reduce((sum, stavka: CartProizvodDTO) => sum + (stavka.vrijednost || 0), 0);
  }
}