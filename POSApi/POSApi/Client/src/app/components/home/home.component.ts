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

  constructor (
    private proizvodiService: ProizvodiService) {}

  ngOnInit(): void {
      this.loadProizvodi
  }

  loadProizvodi(): void {
    this.proizvodiService.getAllProizvodi().subscribe(proizvodi => {
      console.log('Dohvaceni proizvodi: ', proizvodi);
      this.proizvodi = proizvodi;
    });
  }

}







//<!--  ' '  <div>  []  {}  || -->