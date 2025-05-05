import { Component, OnInit } from '@angular/core';
import { CreateProizvodDTO, GetProizvodDTO, UpdateProizvodDTO } from '../../models/proizvodi';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ProizvodiService } from '../../core/services/proizvod.service';
import { MessageService } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { CardModule } from 'primeng/card';
import { CommonModule } from '@angular/common';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';
import { DecodedToken } from '../../models/decodeToken';
import { FileUploadModule } from 'primeng/fileupload';


@Component({
  selector: 'app-proizvodi',
  standalone: true,
  imports: [TableModule, ReactiveFormsModule, ButtonModule, DialogModule, CardModule, CommonModule, FileUploadModule],
  templateUrl: './proizvodi.component.html',
  styleUrl: './proizvodi.component.scss'
})

export class ProizvodiComponent implements OnInit{

  proizvodi: GetProizvodDTO[] = [];
  selectedProizvod: GetProizvodDTO | null = null;
  proizvodForm!: FormGroup;
  searchControl = new FormControl;
  selectedFile: File | null = null;
  imagePreview: string | null = null;

  constructor (
    private proizvodiService: ProizvodiService,
    private fb: FormBuilder,
    private messageService: MessageService,
    private router: Router

  ) {}

  ngOnInit(): void {

    this.loadProizvodi();
    this.proizvodForm = this.fb.group({
      sifra: [null, Validators.required],
      naziv: ['',[Validators.required, Validators.maxLength(100)]],
      jedinica_mjere: ['', [Validators.required, Validators.maxLength(100)]],
      cijena: [null, [Validators.required, Validators.minLength(0)]],
      stanje: [null, [Validators.required, Validators.minLength(0)]],
      popust: [0, [Validators.min(0), Validators.max(100)]],
      proizvodSlikaUrl: ['', Validators.required]
    });

    //Search bar.
    this.searchControl.valueChanges
    .pipe(debounceTime(500), distinctUntilChanged())
    .subscribe((sifra: string) => {
      if (!sifra || sifra.trim() === '') {
        this.loadProizvodi();
        return;
      }

      const sifraBroj = Number(sifra);
      if (!isNaN(sifraBroj)) {
        this.proizvodiService.findProizvodBySifra(sifraBroj).subscribe({
          next: (proizvod) => this.proizvodi = [proizvod],
          error: () => this.proizvodi = []
        });
      } else {
        this.proizvodi = [];
      }
    });
  }

  loadProizvodi(): void {
    this.proizvodiService.getAllProizvodi().subscribe(proizvodi => {

      this.proizvodi = proizvodi.map(p => ({
        ...p,
        proizvodSlikaUrl: `/images/${p.proizvodSlikaUrl}`
      }));
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
      popust: proizvod.popust,
      proizvodSlikaUrl: proizvod.proizvodSlikaUrl
    });
    this.proizvodDialog = true;
  }

  onUpload(event: Event): void {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      const imageUrl = '/images/' + fileInput.files[0].name;

      this.proizvodForm.patchValue({
        proizvodSlikaUrl: imageUrl
      });
    }
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