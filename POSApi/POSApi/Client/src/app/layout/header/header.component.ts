import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BadgeModule } from 'primeng/badge';
import { OverlayBadgeModule } from 'primeng/overlaybadge';
import { ButtonModule } from 'primeng/button';
import { RouterModule } from '@angular/router';
import { ThemeService } from '../../core/services/theme.service';
import { AuthService } from '../../core/services/auth.service';
import { CommonModule, NgIf } from '@angular/common';


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    BadgeModule,
    OverlayBadgeModule,
    ButtonModule,
    RouterModule,
    CommonModule,
    NgIf
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})

export class HeaderComponent {
  theme: string;
  themeIcon: string;

  constructor(
    private router: Router,
    private themeService : ThemeService,
    public authService: AuthService
  ) {
    this.theme = this.themeService.getCurrentTheme();
    this.themeIcon = this.getThemeIcon(this.theme);

  }

  toggleTheme() {
    this.theme = this.themeService.switchTheme();
    this.themeIcon = this.getThemeIcon(this.theme);
  }

  getThemeIcon(theme: string): string {
    return theme.includes('dark') ? 'pi pi-sun' : 'pi pi-moon';
  }

  logOut() {
    sessionStorage.clear();
    this.router.navigate(['login']);
  }
}
