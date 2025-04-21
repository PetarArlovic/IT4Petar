import { Component } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { Card, CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth.component';
import { MessageService } from 'primeng/api';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CardModule,InputTextModule,ReactiveFormsModule, ButtonModule,RouterModule,CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private messageService: MessageService)
    {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    })
  }

  get email () {return this.loginForm.controls['email'];}
  get password () {return this.loginForm.controls['password'];}

  loginUser() {
    const {email, password} = this.loginForm.value;
    this.authService.loginUser({email: email, password: password}).subscribe(
      (response: any) => {
          sessionStorage.setItem('token', response.token);
          this.router.navigate(['/home'])
      },
      (error: HttpErrorResponse) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Nesto nije u redu', life: 3000 });
      }
    )
  }
}
//<!--  ' '  <div>  []  {}  || -->