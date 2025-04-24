import { Component, OnInit } from '@angular/core';
import { CreateProizvodDTO, GetProizvodDTO, UpdateProizvodDTO } from '../../models/proizvodi';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProizvodiService } from '../../core/services/proizvod.service';
import { MessageService } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { CardModule } from 'primeng/card';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-proizvodi',
  imports: [TableModule, ReactiveFormsModule, ButtonModule, DialogModule, CardModule, CommonModule],
  templateUrl: './proizvodi.component.html',
  styleUrl: './proizvodi.component.scss'
})

export class ProizvodiComponent implements OnInit{

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
      sifra: [null, Validators.required],
      naziv: ['',[Validators.required, Validators.maxLength(100)]],
      jedinica_mjere: ['', [Validators.required, Validators.maxLength(100)]],
      cijena: [null, [Validators.required, Validators.minLength(0)]],
      stanje: [null, [Validators.required, Validators.minLength(0)]],
      proizvodSlikaUrl: ['', Validators.required]
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
        this.proizvodiService.updateProizvod(this.selectedProizvod.sifra, proizvodData).subscribe(() => {
          this.messageService.add({ severity: 'success', summary: 'Uspješno', detail: 'Proizvod ažuriran.' });
          this.loadProizvodi();
          this.hideDialog();
        });
      }

      else {
        this.proizvodiService.addProizvod(proizvodData).subscribe(() => {
          this.messageService.add({ severity: 'success', summary: 'Uspješno', detail: 'Proizvod dodan.' });
          this.loadProizvodi();
          this.hideDialog();
        });
      }
    }
  }

  addProizvod(proizvod: CreateProizvodDTO): void{
    this.proizvodiService.addProizvod(proizvod).subscribe(()=>{
      this.messageService.add({ severity: 'success', summary: 'Uspješno', detail: 'Proizvod je dodan.' });
      this.loadProizvodi();
      this.hideDialog();
    });
  }

  updateProizvod(sifra: number, proizvod:UpdateProizvodDTO): void{
    this.proizvodiService.updateProizvod(sifra, proizvod).subscribe(()=>{
      this.messageService.add({ severity: 'success', summary: 'Uspješno', detail: 'Proizvod je azuriran.' });
      this.loadProizvodi();
    })
  }

  deleteProizvod(sifra: number): void{
    this.proizvodiService.deleteProizvod(sifra).subscribe(()=>{
      this.messageService.add({ severity: 'success', summary: 'Uspješno', detail: 'Proizvod je obrisan.' });
      this.loadProizvodi()
    })
  }

  editProizvod(proizvod: GetProizvodDTO): void{
    this.selectedProizvod = proizvod;
    this.proizvodForm.patchValue({
      sifra: proizvod.sifra,
      naziv: proizvod.naziv,
      jedinica_mjere: proizvod.jedinica_mjere,
      cijena: proizvod.cijena,
      stanje: proizvod.stanje,
      proizvodSlikaUrl: proizvod.proizvodSlikaUrl
    });
    this.proizvodDialog = true;
  }

  openNew(): void {
    this.proizvodForm.reset();
    this.selectedProizvod = null;
    this.proizvodDialog = true;
  }

  proizvodDialog: boolean = false;

  hideDialog(): void {
    this.proizvodDialog = false;
  }
}


//<!--  ' '  <div>  []  {}  || -->