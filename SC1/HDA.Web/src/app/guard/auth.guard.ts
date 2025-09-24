import { createInjectableType } from '@angular/compiler';
import { CanActivateFn, Route, Router } from '@angular/router';
import { AuthenticationService } from '../service/authentication.service';
import { inject } from '@angular/core';
import { catchError, map, of } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const authSrv = inject(AuthenticationService);
  const router = inject(Router);

  return new Promise((resolve, reject) => {
    authSrv.isConnectedPreview$.subscribe(b => { 
        resolve(b)
        if (!b) { router.navigateByUrl('/match')}
      })
  })
  
};
