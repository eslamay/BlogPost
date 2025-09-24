import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { inject } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import {jwtDecode} from 'jwt-decode';
export const authGuard: CanActivateFn = (route, state) => {
   const authService = inject(AuthService);
   const router = inject(Router);
   const cookies = inject(CookieService);
   const user = authService.getUser();

   var token = cookies.get('authorization');
   if (token&&user) {
    token = token.replace('Bearer ', '');
    const decodedToken:any=jwtDecode(token);
    if (decodedToken.exp < Date.now() / 1000) {
      authService.logout();
      return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } });
    }else{
      if (user.roles.includes('Writer')) {
        return true;
      } else {
        alert('You do not have permission to access this page.');
        return router.createUrlTree(['/']);
      }
    }  
   }
   else {
    authService.logout();
    return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } });
   }
};
