import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss'
})
export class KontaktFormaComponent {
  ime: string = '';
  email: string = '';
  poruka: string = '';

  onSubmit() {
    console.log('Poruka poslana:', {
      ime: this.ime,
      email: this.email,
      poruka: this.poruka
    });

    this.ime = '';
    this.email = '';
    this.poruka = '';
  }
}
