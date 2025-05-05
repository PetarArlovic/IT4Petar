import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { GetStavke_racunaDTO } from '../../models/stavke_racuna';

@Injectable({
  providedIn: 'root'
})

export class ShopRegisterService {
  stavke: GetStavke_racunaDTO[] = [];
  totalCost: number = 0;
  totalDiscount: number = 0;

  constructor() {
    this.stavke = [];
    this.totalCost = 0;
    this.totalDiscount = 0;
  }

  addStavka(stavka: GetStavke_racunaDTO) {
    const ukupnoBezPopusta = stavka.cijena * stavka.kolicina;
    stavka.iznos_popusta = (stavka.popust / 100) * ukupnoBezPopusta;
    stavka.vrijednost = ukupnoBezPopusta - stavka.iznos_popusta;

    this.stavke.push(stavka);
  }

  calculateTotal(): number {
    const total = this.stavke.reduce((sum, stavka) => {
      const ukupnoBezPopusta = stavka.cijena * stavka.kolicina;
      const iznosPopusta = (stavka.popust / 100) * ukupnoBezPopusta;
      const ukupno = ukupnoBezPopusta - iznosPopusta;
      return sum + ukupno;
    }, 0);

    return parseFloat(total.toFixed(2));
  }
}