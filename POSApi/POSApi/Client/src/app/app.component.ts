import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ToastModule } from 'primeng/toast';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ThemeService } from './core/services/theme.service';
import { HeaderComponent } from './layout/header/header.component';
import { environment } from './environments/environment';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    CommonModule,
    HeaderComponent,
    ToastModule,],

  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppComponent {

  baseUrl = environment.apiUrl
  private http = inject(HttpClient);
  title = 'Client';

  constructor(
    public router: Router,
    private themeService : ThemeService
  ) {}

  ngOnInit() {
    this.themeService.loadTheme(this.themeService.getCurrentTheme());
  }
}

