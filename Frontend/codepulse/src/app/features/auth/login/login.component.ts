import { Component, inject } from '@angular/core';
import { LoginRequest } from './models/login-request.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  private authService = inject(AuthService);
  private cookieService = inject(CookieService);
  private router = inject(Router);
  model: LoginRequest;

  constructor() {
    this.model = { email: '', password: '' };
  }

  onFormSubmit() {
    this.authService.login(this.model).subscribe({
      next: (response) => {
        this.cookieService.set(
          'authorization',
          `Bearer ${response.token}`,
          undefined, '/',undefined,true, 'Strict'
        );
        this.authService.setUser({
          email: response.email,
          roles: response.roles
        });
        this.router.navigate(['/']);
      },
    });
  }
}
