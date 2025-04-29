import { Component, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ProizvodiService } from '../../core/services/proizvod.service';
import { GetProizvodDTO } from '../../models/proizvodi';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { CardModule } from 'primeng/card';
import { RegisterComponent } from '../register/register.component';
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
  kosarica: GetProizvodDTO[] = [];

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

  dodajUKosaricu(proizvod: GetProizvodDTO) {
    this.kosarica.push(proizvod);
  }

}







//<!--  ' '  <div>  []  {}  || -->