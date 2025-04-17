import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeng/themes/aura';
import { provideHttpClient } from '@angular/common/http';
import { MessageService } from 'primeng/api';


import { routes } from './app.routes';
import { AuthComponent } from './services/auth/auth.component';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(),
        providePrimeNG({
            theme: {
                preset: Aura,
            },
        }),
        MessageService,
        AuthComponent
  ]
};

//<!--  ' '  <div>  []  {}  || -->