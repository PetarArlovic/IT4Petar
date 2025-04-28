import { Component, OnInit } from '@angular/core';
import { shopRegister } from '../../models/shopRegister';
import { Observable } from 'rxjs';
import { CreateStavke_racunaDTO, GetStavke_racunaDTO } from '../../models/stavke_racuna';
import { StavkeRacunaService } from '../../core/services/stavke-racuna.service';
import { ZaglavljeRacunaService } from '../../core/services/zaglavlje-racuna.service';
import { getLocaleFirstDayOfWeek } from '@angular/common';
import { CreateZaglavlje_racunaDTO } from '../../models/zaglavlje_racuna';
import { KupciService } from '../../core/services/kupci.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GetKupacDTO } from '../../models/kupci';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop-register',
  imports: [ButtonModule, CardModule, ReactiveFormsModule, TableModule, DialogModule, FormsModule],
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

  kupacForm: FormGroup;
  selectedStavka: GetStavke_racunaDTO | null = null;
  totalCost: number = 0;
  displayDialog: boolean = false;
  napomena: string = '';

  constructor(
    private stavkeRacunaService: StavkeRacunaService,
    private zaglavljeRacunaService: ZaglavljeRacunaService,
    private kupciService: KupciService,
    private fb: FormBuilder
  ){
    this.kupacForm = this.fb.group({
      sifra: ['', Validators.required],
      naziv: ['', Validators.required],
      adresa: ['', Validators.required],
      mjesto: ['', Validators.required]
    })
  }

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
      this.updateTotalCost();
  }

  updateTotalCost(): void {
    this.totalCost = this.register.calculateTotal();
  }

  CreateNewBill(): void {
    if (this.kupacForm.valid) {
      this.zaglavlje.napomena = this.kupacForm.value.napomena || '';
      this.zaglavlje.kupacId = this.kupacForm.value.kupacId;

      this.zaglavljeRacunaService.addZaglavljeRacuna(this.zaglavlje).subscribe((result) => {
        this.brojRacuna = result.broj;
        this.addStavkeToRacun();
      });
    } else {
      alert ('Ispunite sve podatke o kupcu!');
    }
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

  confirmTransaction(): void {
    this.zaglavlje.napomena = this.napomena;
    this.zaglavljeRacunaService.addZaglavljeRacuna(this.zaglavlje).subscribe((result) => {
      alert('Transakcija uspješno izvršena!');
      this.displayDialog = false;
    });
  }

  cancelTransaction(): void {
    this.displayDialog = false;
  }
}






//<!--  ' '  <>  []  {}  || -->