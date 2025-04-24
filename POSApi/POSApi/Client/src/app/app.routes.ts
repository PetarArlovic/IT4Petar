import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { ButtonDemo } from './button-test/button-test.component';
import { authGuard } from './guards/auth.guard';
import { ProizvodiComponent } from './components/proizvodi/proizvodi.component';

export const routes: Routes = [
    {
    path: 'login',
    component: LoginComponent
    },

    {
    path: 'register',
    component: RegisterComponent
    },

    {
    path: 'home',
    component: HomeComponent,
    canActivate: [authGuard]
    },

    {
    path: 'Proizvodi',
    component: ProizvodiComponent
    },

    {
    path: 'Button',
    component: ButtonDemo
    },

    {
    path: '', redirectTo: '/home', pathMatch: 'full'
    }
];
