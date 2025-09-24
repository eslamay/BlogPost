import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../features/auth/services/auth.service';
import { User } from '../../../features/auth/login/models/user.model';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  
  user?:User;

  private authService = inject(AuthService);
  private router = inject(Router);

  ngOnInit() {
    this.authService.user().subscribe(user => {
      this.user = user;
    });

    this.user = this.authService.getUser();
  }


  logout(){
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
