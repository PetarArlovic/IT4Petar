import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { authGuard } from './guards/auth.guard';
import { ProizvodiComponent } from './components/proizvodi/proizvodi.component';
import { TransactionsComponent } from './components/transactions/transactions.component';

export const routes: Routes = [
    {
    path: 'login',
    component: LoginComponent,
    canActivate: [authGuard],
    data: { public: true }
    },

    {
    path: 'register',
    component: RegisterComponent,
    canActivate: [authGuard],
    data: { public: true }
    },

    {
    path: 'home',
    component: HomeComponent,
    canActivate: [authGuard],
    data: { userAllowed: true }
    },

    {
    path: 'Proizvodi',
    component: ProizvodiComponent,
    canActivate: [authGuard],
    },

    {
    path: 'Transakcije',
    component: TransactionsComponent,
    canActivate: [authGuard],
    },

    {
    path: '', redirectTo: '/login', pathMatch: 'full'
    },

];
