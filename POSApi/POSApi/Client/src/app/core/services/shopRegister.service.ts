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
    const cijenaSPopustom = stavka.cijena - (stavka.cijena * (stavka.popust / 100));
    stavka.vrijednost = cijenaSPopustom * stavka.kolicina;
    stavka.iznos_popusta = (stavka.cijena - cijenaSPopustom) * stavka.kolicina;

    this.stavke.push(stavka);
    this.updateTotals();
  }

  updateTotals() {
    this.totalCost = this.stavke.reduce((sum, stavka) => sum + stavka.vrijednost, 0);
    this.totalDiscount = this.stavke.reduce((sum, stavka) => sum + stavka.iznos_popusta, 0);
  }

  calculateTotal(): number {
    return parseFloat(this.totalCost.toFixed(2));
  }
}