import { Component, OnInit } from '@angular/core';
import { CreateProizvodDTO, GetProizvodDTO, UpdateProizvodDTO } from '../../models/proizvodi';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProizvodiService } from '../../core/services/proizvod.service';
import { MessageService } from 'primeng/api';
import { UpdateKupacDTO } from '../../models/kupci';

@Component({
  selector: 'app-proizvodi',
  imports: [],
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
      SIFRA: [null, Validators.required],
      NAZIV: ['',[Validators.required, Validators.maxLength(100)]],
      JEDINICA_MJERE: [['', [Validators.required, Validators.maxLength(100)]]],
      CIJENA: [null, [Validators.required, Validators.minLength(0)]],
      STANJE: [null, [Validators.required, Validators.minLength(0)]],
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

  addProizvod(proizvod: CreateProizvodDTO): void{
    this.proizvodiService.addProizvod(proizvod).subscribe(()=>{
      this.messageService.add({ severity: 'success', summary: 'Uspješno', detail: 'Proizvod je dodan.' });
    });
  }

  updateProizvod(sifra: number, proizvod:UpdateProizvodDTO): void{
    this.proizvodiService.updateProizvod(sifra, proizvod).subscribe(()=>{
      this.messageService.add({ severity: 'success', summary: 'Uspješno', detail: 'Proizvod je azuriran.' });
    })
  }

  deleteProizvod(id: number): void{
    this.proizvodiService.deleteProizvod(id).subscribe(()=>{
      this.messageService.add({ severity: 'success', summary: 'Uspješno', detail: 'Proizvod je obrisan.' });
    })
  }
}







//<!--  ' '  <div>  []  {}  || -->