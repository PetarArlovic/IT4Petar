import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { CreateStavke_racunaDTO, GetStavke_racunaDTO } from '../../models/stavke_racuna';
import { StavkeRacunaService } from '../../core/services/stavke-racuna.service';
import { ZaglavljeRacunaService } from '../../core/services/zaglavlje-racuna.service';
import { ShopRegisterService } from '../../core/services/shopRegister.service';
import { CommonModule, getLocaleFirstDayOfWeek } from '@angular/common';
import { CreateZaglavlje_racunaDTO, GetZaglavlje_racunaDTO } from '../../models/zaglavlje_racuna';
import { KupciService } from '../../core/services/kupci.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreateKupacDTO, GetKupacDTO } from '../../models/kupci';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { FormsModule } from '@angular/forms';
import { CartProizvodDTO, GetProizvodDTO } from '../../models/proizvodi';
import { BadgeModule } from 'primeng/badge';
import { OverlayPanelModule } from 'primeng/overlaypanel';

@Component({
  selector: 'app-shop-register',
  imports: [
    ButtonModule,
    CardModule,
    ReactiveFormsModule,
    TableModule,
    DialogModule,
    FormsModule,
    CommonModule,
    OverlayPanelModule,
    BadgeModule
  ],

  standalone: true,
  templateUrl: './shop-register.component.html',
  styleUrl: './shop-register.component.scss'
})
export class ShopRegisterComponent implements OnInit, OnChanges {
    stavke: GetStavke_racunaDTO[] = [];
    brojRacuna?: number;
    kupci: GetKupacDTO[] = [];
    proizvodi: GetProizvodDTO[] = [];
    @Input() kosarica: CartProizvodDTO[] = [];

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
    showCart = false;

    constructor(
      private stavkeRacunaService: StavkeRacunaService,
      private zaglavljeRacunaService: ZaglavljeRacunaService,
      private kupciService: KupciService,
      private fb: FormBuilder,
      private register: ShopRegisterService
    ){
      this.kupacForm = this.fb.group({
        naziv: ['', Validators.required],
        adresa: ['', Validators.required],
        mjesto: ['', Validators.required],
        napomena: ['']
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

      if (this.kosarica.length === 0) {
        alert('Morate dodati barem jedan proizvod u košaricu prije nego što možete napraviti račun!');
        return;
      }

      if (this.kupacForm.invalid) {
        alert('Ispunite sve podatke o kupcu!');
        return;
      }

      this.register = new ShopRegisterService();

      this.saveKupac().then(() => {
        this.initializeStavkeFromKosarica();
        this.updateTotalCost();
        this.addStavkeToRacun();
      }).finally(() => {
        this.resetFormAndCart();
        this.displayDialog = true;
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

        this.stavkeRacunaService.addStavkeRacuna(novaStavka).subscribe({
        next: (result) => {
          console.log('Stavka dodana na račun:', result);
          alert('Novi račun kreiran!');
        },
        error: (err) => {
          console.error('Greška pri dodavanju stavke na račun:', err);
          alert('Greška pri dodavanju stavke!');
        }
      });
    });
  }

    resetTransaction(): void {
      this.register = new ShopRegisterService();
      this.stavke = [];
      this.totalCost = 0;
      this.kupacForm.reset();
      this.napomena = '';
      this.displayDialog = false;
    }

    removeStavkaFromRegister(index: number) {
      this.kosarica.splice(index, 1);
      this.updateTotalCost();
      this.initializeStavkeFromKosarica();
    }

    ngOnChanges(changes: SimpleChanges): void {
      if (changes['kosarica']) {
        this.initializeStavkeFromKosarica();
        this.updateTotalCost();
        console.log('Košarica je ažurirana: ', this.kosarica);
      }
    }

    initializeStavkeFromKosarica(): void {
      this.register = new ShopRegisterService();
      this.kosarica.forEach((proizvod, index) => {
        const ukupnoBezPopusta = proizvod.cijena * proizvod.kolicina;
        const iznos_popusta = (proizvod.popust / 100) * ukupnoBezPopusta;
        const vrijednost = ukupnoBezPopusta - iznos_popusta;

        const stavka: GetStavke_racunaDTO = {
          broj: index + 1,
          proizvodId: proizvod.sifra,
          cijena: proizvod.cijena,
          kolicina: proizvod.kolicina,
          popust: proizvod.popust,
          vrijednost: proizvod.cijena,
          iznos_popusta: 0,
        };
        this.register.addStavka(stavka);
      });
      this.stavke = this.register.stavke;
      this.updateTotalCost();
    }


    async saveZaglavlje(): Promise<void> {
      try {
        const savedZag = await firstValueFrom(this.zaglavljeRacunaService.addZaglavljeRacuna(this.zaglavlje));

        if (!savedZag?.broj) {
          throw new Error('Zaglavlje nije ispravno spremljeno.');
        }

        this.brojRacuna = savedZag.broj;
        this.addStavkeToRacun();

      } catch (err) {
        console.error('Greška pri kreiranju zaglavlja:', err);
        alert('Greška pri kreiranju zaglavlja!');
      }
    }

    async saveKupac(): Promise<void> {
    const noviKupac: CreateKupacDTO = {
      naziv: this.kupacForm.value.naziv,
      adresa: this.kupacForm.value.adresa,
      mjesto: this.kupacForm.value.mjesto
    };

    try {
      const savedKupac = await firstValueFrom(this.kupciService.addKupac(noviKupac));

      if (!savedKupac?.id) {
        throw new Error('Greška: Kupac nije ispravno spremljen.');
      }

      await new Promise(resolve => setTimeout(resolve, 300));
      const verifiedKupac = await firstValueFrom(this.kupciService.getKupacById(savedKupac.id));

      if (!verifiedKupac?.id) {
        throw new Error('Greška: Kupac nije uspješno potvrđen.');
      }

      console.log('Kupac spremljen s ID:', savedKupac.id);

      this.zaglavlje.kupacId = savedKupac.id;
      this.zaglavlje.napomena = this.kupacForm.value.napomena || '';

      await this.saveZaglavlje();

    } catch (err) {
      console.error('Greška pri spremanju kupca:', err);
      alert('Greška pri spremanju kupca!');
    }
  }

  resetFormAndCart(): void {
    this.kosarica = [];
    this.kupacForm.reset();
    this.totalCost = 0;
  }
}






//<!--  ' '  <>  []  {}  || -->