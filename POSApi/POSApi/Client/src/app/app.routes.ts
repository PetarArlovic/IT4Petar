import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { ButtonDemo } from './button-test/button-test.component';

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
    component: HomeComponent
    },

    {
    path: 'Button',
    component: ButtonDemo
    },
    
    {
    path: '', redirectTo: '/home', pathMatch: 'full'
    }
];
