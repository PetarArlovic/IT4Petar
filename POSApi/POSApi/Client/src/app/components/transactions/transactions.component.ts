import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { GetZaglavlje_racunaDTO } from '../../models/zaglavlje_racuna';
import { GetStavke_racunaDTO } from '../../models/stavke_racuna';
import { ZaglavljeRacunaService } from '../../core/services/zaglavlje-racuna.service';
import { StavkeRacunaService } from '../../core/services/stavke-racuna.service';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { DialogModule } from 'primeng/dialog';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';


@Component({
  selector: 'app-transactions',
  imports: [
    ButtonModule,
    CardModule,
    TableModule,
    DialogModule,
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.scss'
})
export class TransactionsComponent implements OnInit {
  zaglavlja: GetZaglavlje_racunaDTO[] = [];
  stavke: GetStavke_racunaDTO[] = [];
  selectedZaglavlje: GetZaglavlje_racunaDTO | null = null;
  filteredZaglavlja: GetZaglavlje_racunaDTO[] = [];
  displayDialog: boolean = false;
  searchControl = new FormControl('');

  constructor(
    private zaglavljeService: ZaglavljeRacunaService,
    private stavkeService: StavkeRacunaService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
      this.loadTransactions();

      this.searchControl.valueChanges
    .pipe(debounceTime(500), distinctUntilChanged())
    .subscribe((value) => {
      if (!value || value.trim() === '') {
        this.zaglavlja = this.filteredZaglavlja;
        return;
      }

      const brojRacuna = Number(value);
      if (isNaN(brojRacuna)) {
        this.messageService.add({
          severity: 'warn',
          summary: 'Neispravan unos',
          detail: 'Broj računa mora biti broj.'
        });
        this.zaglavlja = [];
        return;
      }

      this.zaglavljeService.getZaglavljeRacunaByBroj(brojRacuna).subscribe({
        next: (zaglavlje) => {
          this.zaglavlja = zaglavlje ? [zaglavlje] : [];
          if (this.zaglavlja.length === 0) {
            this.messageService.add({
              severity: 'info',
              summary: 'Nema rezultata',
              detail: `Nije pronađen račun s brojem ${brojRacuna}.`
            });
          }
        },
        error: (err) => {
          console.error('Greška pri pretraživanju računa', err);
          this.messageService.add({
            severity: 'error',
            summary: 'Greška',
            detail: 'Greška pri pretraživanju računa.'
          });
          this.zaglavlja = [];
        }
      });
    });
  }

  loadTransactions(): void {
    this.zaglavljeService.getAllZaglavlja().subscribe({
      next: (data) => {
        console.log('Zaglavlja:', data);
        this.zaglavlja = data;
        this.filteredZaglavlja = data;
      },
      error: (err) => {
        console.error('Greška pri učitavanju transakcija', err);
      }
    });
  }

  showStavke(zaglavlje: GetZaglavlje_racunaDTO): void {
    this.selectedZaglavlje = zaglavlje;
    this.stavkeService.GetStavkeRacunaByBroj(zaglavlje.broj).subscribe({
      next: (data) => {
        this.stavke = data;
        this.displayDialog = true;
      },
      error: (error) => {
        console.error('Greška pri učitavanju stavki, err')
      }
    });
  }
}