import { Component } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { Card, CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { potvrdaSifreV } from '../../shared/potvrda-sifre.directive';
import { AuthService } from '../../core/services/auth.component';
import { User } from '../../interfaces/auth';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CardModule,
    InputTextModule,
    ReactiveFormsModule,
    ButtonModule,
    RouterModule,
    CommonModule,
    ToastModule,],

  providers: [MessageService],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})


export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private messageService: MessageService,
    private router: Router)

  {
    this.registerForm = this.fb.group(
    {
      ime: ['', [Validators.required, Validators.pattern(/^[a-zA-ZćčžšđĆČŽŠĐ]+$/)]],
      prezime: ['', [Validators.required, Validators.pattern(/^[a-zA-ZćčžšđĆČŽŠĐ]+$/)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      potvrdiSifru: ['', [Validators.required,]],
    },
    {
      validators: potvrdaSifreV
    })
  }

  //Controller form methods
  get ime () {return this.registerForm.controls['ime'];}
  get prezime () {return this.registerForm.controls['prezime'];}
  get email () {return this.registerForm.controls['email'];}
  get password () {return this.registerForm.controls['password'];}
  get potvrdiSifru () {return this.registerForm.controls['potvrdiSifru']}


  submitDetails() {
    const postData = { ...this.registerForm.value};
    delete postData.potvrdiSifru;
    this.authService.registerUser(postData as User).subscribe(
      response => {console.log(response);
      this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Registracija uspjesna', life: 3000 });
      this.router.navigate(['login'])
      },
      error => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Nesto nije u redu', life: 3000 });
      }
    )
  }
}
//<!--  ' '  <div>  []  {}  || -->

