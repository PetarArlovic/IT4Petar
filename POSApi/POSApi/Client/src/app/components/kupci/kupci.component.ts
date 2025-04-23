import { Component } from '@angular/core';
import { GetKupacDTO } from '../../models/kupci';
import { KupciService } from '../../core/services/kupci.service';

@Component({
  selector: 'app-kupci',
  imports: [],
  templateUrl: './kupci.component.html',
  styleUrl: './kupci.component.scss'
})
export class KupciComponent {
  kupci: GetKupacDTO[] = [];

  constructor(private kupacService: KupciService){}

  ngOnInit(): void {
    this.kupacService.getAllKupci().subscribe((kupci) => {
      this.kupci = kupci
    })
  }
}

//<!--  ' '  <div>  []  {}  || -->