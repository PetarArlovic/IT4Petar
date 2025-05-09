import { Component, OnInit } from '@angular/core';
import { GetKupacDTO } from '../../models/kupci';
import { KupciService } from '../../core/services/kupci.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-kupci',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './kupci.component.html',
  styleUrl: './kupci.component.scss'
})
export class KupciComponent implements OnInit {
  kupci: GetKupacDTO[] = [];

  constructor(private kupacService: KupciService){}

  ngOnInit(): void {
    this.kupacService.getAllKupci().subscribe((kupci) => {
      this.kupci = kupci
    })
  }
}