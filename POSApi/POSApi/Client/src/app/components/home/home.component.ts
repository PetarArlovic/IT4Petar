import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ProizvodiService } from '../../core/services/proizvod.service';
import { CartProizvodDTO, GetProizvodDTO } from '../../models/proizvodi';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { CardModule } from 'primeng/card';
import { CommonModule } from '@angular/common';
import { ShopRegisterComponent } from '../shop-register/shop-register.component';

@Component({
  selector: 'app-home',
  imports: [ButtonModule, CardModule, CommonModule, ShopRegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})

export class HomeComponent implements OnInit {

  proizvodi: GetProizvodDTO[] = [];
  kosarica: CartProizvodDTO[] = [];
  totalCost: number = 0;

  constructor (
    private proizvodiService: ProizvodiService) {}

  ngOnInit(): void {
      this.loadProizvodi();
  }

  loadProizvodi(): void {
    this.proizvodiService.getAllProizvodi().subscribe(proizvodi => {
      console.log('Dohvaćeni proizvodi: ', proizvodi);

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
      this.kosarica = [...this.kosarica];
    } else {
      const novaStavka: CartProizvodDTO = {
        ...proizvod,
        kolicina: 1,
        popust: 0
      };
      this.kosarica = [...this.kosarica, novaStavka];
    }

    this.recalculateTotal();
  }

  private recalculateTotal() {
    this.totalCost = this.kosarica
    .reduce((sum, stavka: CartProizvodDTO) => sum + stavka.cijena * stavka.kolicina * (1 - stavka.popust / 100), 0);
  }
}







//<!--  ' '  <div>  []  {}  || -->