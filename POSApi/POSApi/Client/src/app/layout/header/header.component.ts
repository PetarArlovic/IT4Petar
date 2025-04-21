import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BadgeModule } from 'primeng/badge';
import { OverlayBadgeModule } from 'primeng/overlaybadge';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-header',
  imports: [
    BadgeModule,
    OverlayBadgeModule,
    ButtonModule],

  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})

export class HeaderComponent {
  constructor (private router: Router) {}

  logOut() {
    sessionStorage.clear();
    this.router.navigate(['login']);
  }
}
