import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ProizvodiService } from '../../core/services/proizvod.service';
import { GetProizvodDTO } from '../../interfaces/proizvodi';

@Component({
  selector: 'app-home',
  imports: [ButtonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  proizvodi: GetProizvodDTO[] = [];

  constructor (
    private proizvodiService: ProizvodiService
  ) {}

  loadProizvodi(): void {
    this.proizvodiService.getAllProizvodi().subscribe(proizvodi => {
      console.log('Dohvaceni proizvodi: ', proizvodi);
      this.proizvodi = proizvodi;
    });
  }
}







//<!--  ' '  <div>  []  {}  || -->