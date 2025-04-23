import { Component, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ProizvodiService } from '../../core/services/proizvod.service';
import { GetProizvodDTO } from '../../models/proizvodi';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { KupciComponent } from '../kupci/kupci.component';

@Component({
  selector: 'app-home',
  imports: [ButtonModule, KupciComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  proizvodi: GetProizvodDTO[] = [];
  selectedProizvod: GetProizvodDTO | null = null;;
  proizvodForm!: FormGroup;

  constructor (
    private proizvodiService: ProizvodiService,
    private fb: FormBuilder,
    private messageService: MessageService

  ) {}

  ngOnInit(): void {
    this.loadProizvodi();
    this.proizvodForm = this.fb.group({
      SIFRA: [null, Validators.required],
      NAZIV: ['',[Validators.required, Validators.maxLength(100)]],
      JEDINICA_MJERE: [['', [Validators.required, Validators.maxLength(100)]]],
      CIJENA: [null, [Validators.required]],
      STANJE: [null, [Validators.required]],
      PROIZVODSlikaUrl: ['', Validators.required]

    });
  }

  loadProizvodi(): void {
    this.proizvodiService.getAllProizvodi().subscribe(proizvodi => {
      console.log('Dohvaceni proizvodi: ', proizvodi);
      this.proizvodi = proizvodi;
    });
  }

  saveProizvod(): void {
    if (this.proizvodForm.valid){
      const proizvodData = this.proizvodForm.value;
      if (this.selectedProizvod){
        this.proizvodiService.updateProizvod(this.selectedProizvod.SIFRA, proizvodData);
      }
      else {
        this.proizvodiService.addProizvod(proizvodData);
      }
    }
  }
}







//<!--  ' '  <div>  []  {}  || -->