import { Component, OnInit } from '@angular/core';
import { shopRegister } from '../../models/shopRegister';
import { Observable } from 'rxjs';
import { CreateStavke_racunaDTO, GetStavke_racunaDTO } from '../../models/stavke_racuna';
import { StavkeRacunaService } from '../../core/services/stavke-racuna.service';
import { ZaglavljeRacunaService } from '../../core/services/zaglavlje-racuna.service';
import { getLocaleFirstDayOfWeek } from '@angular/common';
import { CreateZaglavlje_racunaDTO } from '../../models/zaglavlje_racuna';

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
  broj?: number;

  zaglavlje: CreateZaglavlje_racunaDTO = {
    datum: new Date(),
    kupacId: 1,
    napomena: 'Napomena'
  }

  constructor(
    private stavkeRacunaService: StavkeRacunaService,
    private zaglavljeRacunaService: ZaglavljeRacunaService
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
  }


  addStavkaToRegister(stavka: GetStavke_racunaDTO): void{
    this.register.addStavka(stavka);
    this.register.totalCost = this.register.calculateTotal();
  }

  CreateNewBill(): void {
    this.zaglavljeRacunaService.addZaglavljeRacuna(novoZaglavlje).subscribe((result) => {
      this.brojRacuna = result.broj;
      this.addStavkaToRegister();
    });
  }

  addStavkeToRacun(): void {
    if (!this.brojRacuna) return
      this.register.stavke.forEach((stavka) => {
        const novaStavka: CreateStavke_racunaDTO = {
          broj: this.broj!,
          kolicina: stavka.kolicina,
          cijena: stavka.cijena,
          popust: stavka.popust,
          vrijednost: stavka.vrijednost,
          iznos_popusta: stavka.iznos_popusta,
          proizvodId: stavka.proizvodId
        };

        this.stavkeRacunaService.addStavkeRacuna(novaStavka).subscribe();
    });
  }
}




//<!--  ' '  <>  []  {}  || -->