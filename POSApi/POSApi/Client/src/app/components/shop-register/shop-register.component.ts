import { Component, OnInit } from '@angular/core';
import { shopRegister } from '../../models/shopRegister';
import { Observable } from 'rxjs';
import { CreateStavke_racunaDTO, GetStavke_racunaDTO } from '../../models/stavke_racuna';
import { StavkeRacunaService } from '../../core/services/stavke-racuna.service';
import { ZaglavljeRacunaService } from '../../core/services/zaglavlje-racuna.service';
import { getLocaleFirstDayOfWeek } from '@angular/common';
import { CreateZaglavlje_racunaDTO } from '../../models/zaglavlje_racuna';
import { KupciService } from '../../core/services/kupci.service';
import { FormBuilder } from '@angular/forms';
import { GetKupacDTO } from '../../models/kupci';

@Component({
  selector: 'app-shop-register',
  imports: [],
  standalone: true,
  templateUrl: './shop-register.component.html',
  styleUrl: './shop-register.component.scss'
})
export class ShopRegisterComponent implements OnInit {
  register: shopRegister = new shopRegister();
  stavke: GetStavke_racunaDTO[] = [];
  brojRacuna?: number;
  kupci: GetKupacDTO[] = [];

  zaglavlje: CreateZaglavlje_racunaDTO = {
    broj: 0,
    kupacId: 0,
    napomena: ''
  }

  constructor(
    private stavkeRacunaService: StavkeRacunaService,
    private zaglavljeRacunaService: ZaglavljeRacunaService,
    private kupciService: KupciService,
    private fb: FormBuilder
  ){}

  ngOnInit(): void {
    this.stavkeRacunaService.getAllStavke().subscribe({
      next: (data) => {
        this.stavke = data;
      },
      error: (err) => {
        console.error('Greška prilikom dohvaćanja stavki:', err);
      }
    });

    this.kupciService.getAllKupci().subscribe({
      next: (data) => {
        this.kupci = data;
      },
      error: (err) => {
        console.error('Greška prilikom dohvaćanja kupaca:', err);
      }
    });
  }


  addStavkaToRegister(stavka: GetStavke_racunaDTO): void{
    this.register.addStavka(stavka);
    this.register.totalCost = this.register.calculateTotal();
  }

  CreateNewBill(): void {
    this.zaglavljeRacunaService.addZaglavljeRacuna(this.zaglavlje).subscribe((result) => {
      this.brojRacuna = result.broj;
      this.addStavkeToRacun();
    });
  }

  addStavkeToRacun(): void {
    if (!this.brojRacuna) return
      this.register.stavke.forEach((stavka) => {
        const novaStavka: CreateStavke_racunaDTO = {
          broj: this.brojRacuna!,
          kolicina: stavka.kolicina,
          cijena: stavka.cijena,
          popust: stavka.popust,
          vrijednost: stavka.vrijednost,
          iznos_popusta: stavka.iznos_popusta,
          proizvodId: stavka.proizvodId,
          sifra: stavka.proizvodId
        };

        this.stavkeRacunaService.addStavkeRacuna(novaStavka).subscribe();
    });
  }
}




//<!--  ' '  <>  []  {}  || -->