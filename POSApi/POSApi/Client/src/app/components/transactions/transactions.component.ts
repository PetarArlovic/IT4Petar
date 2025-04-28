import { Component, OnInit } from '@angular/core';
import { GetZaglavlje_racunaDTO } from '../../models/zaglavlje_racuna';
import { GetStavke_racunaDTO } from '../../models/stavke_racuna';
import { ZaglavljeRacunaService } from '../../core/services/zaglavlje-racuna.service';
import { StavkeRacunaService } from '../../core/services/stavke-racuna.service';

@Component({
  selector: 'app-transactions',
  imports: [],
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
        this.zaglavlja = data;
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
        this.stavke = [data]
        this.displayDialog = true;
      },
      error: (error) => {
        console.error('Greška pri učitavanju stavki, err')
      }
    });
  }
}









//<!--  ' '  <>  []  {}  || -->