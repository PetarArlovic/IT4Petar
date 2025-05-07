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


@Component({
  selector: 'app-transactions',
  imports: [
    ButtonModule,
    CardModule,
    TableModule,
    DialogModule,
    CommonModule
  ],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.scss'
})
export class TransactionsComponent implements OnInit {
  zaglavlja: GetZaglavlje_racunaDTO[] = [];
  stavke: GetStavke_racunaDTO[] = [];
  selectedZaglavlje: GetZaglavlje_racunaDTO | null = null;
  displayDialog: boolean = false;

  constructor(
    private zaglavljeService: ZaglavljeRacunaService,
    private stavkeService: StavkeRacunaService
  ) {}

  ngOnInit(): void {
      this.loadTransactions();
  }

  loadTransactions(): void {
    this.zaglavljeService.getAllZaglavlja().subscribe({
      next: (data) => {
        console.log('Zaglavlja:', data);
        this.zaglavlja = data;
      },
      error: (err) => {
        console.error('Greška pri učitavanju transakcija', err);
      }
    });
  }

  showStavke(zaglavlje: GetZaglavlje_racunaDTO): void {
    console.log('Zaglavlje:', zaglavlje);
    this.selectedZaglavlje = zaglavlje;
    this.stavkeService.GetStavkeRacunaByBroj(zaglavlje.broj).subscribe({
      next: (data) => {
        console.log('Stavke:', data);
        this.stavke = data;
        this.displayDialog = true;
      },
      error: (error) => {
        console.error('Greška pri učitavanju stavki, err')
      }
    });
  }
}









//<!--  ' '  <>  []  {}  || -->