import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.auth.getJwtToken();
    let cloned = req;
    if (token) {
      cloned = req.clone({ setHeaders: { Authorization: `Bearer ${token}` }});
    }
    // apply withCredentials for cookie-based flows
    if (this.auth.isCookieBased() && !cloned.withCredentials) {
      cloned = cloned.clone({ withCredentials: true });
    }
    return next.handle(cloned);
  }
}
