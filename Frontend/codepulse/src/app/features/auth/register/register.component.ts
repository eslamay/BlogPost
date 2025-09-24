import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule,ReactiveFormsModule,RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
private authService = inject(AuthService);
private router = inject(Router);
private fb=inject(FormBuilder)
form!:FormGroup
ngOnInit(): void {
  this.formValidation()
}

formValidation(){
  this.form=this.fb.group({
     userName:['',[Validators.required,Validators.minLength(6)]],
     email:['',[Validators.required,Validators.email]],
     password:['',[Validators.required,Validators.pattern(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/)]],
  })
}

get UserName(){
  return this.form.get('userName')
}
get Password(){
  return this.form.get('password')
}
get Email(){
  return this.form.get('email')
}

onFormSubmit(){
  if (this.form.valid) {
    this.authService.register(this.form.value).subscribe({
      next:(res)=>{
        console.log(res)
        this.router.navigate(['/']);
      },
      error:(err:any)=>{
        console.log(err)
      }
    })
    
  }
}
}
