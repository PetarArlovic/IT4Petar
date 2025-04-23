import { Component, inject, OnInit } from '@angular/core';
import { GetProizvodDTO } from '../../models/proizvodi';
import { ShopService } from '../../core/services/shop.service';

@Component({
  selector: 'app-shop',
  imports: [],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {

  private shopService = inject(ShopService);
  proizvodi: GetProizvodDTO[] = [];

  ngOnInit(): void {
    this.shopService.getProizvodi().subscribe({
      //next: response => this.products = response.data,
      error: error => console.log(error),
      complete: () => console.log('complete')
    })
  }
}

