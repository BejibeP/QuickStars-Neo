import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private _user$ = new BehaviorSubject<any>(null);
  user$ = this._user$.asObservable();

  private jwt?: string;
  private cookieBased = true; // toggle depending on your backend strategy

  constructor(private http: HttpClient) {}

  isCookieBased() { return this.cookieBased; }

  login(credentials: any) {
    return this.http.post('/api/auth/login', credentials, { withCredentials: this.cookieBased })
      .pipe(tap((res: any) => {
        if (!this.cookieBased && res?.token) this.jwt = res.token;
        // could fetch profile here
      }));
  }

  logout() {
    return this.http.post('/api/auth/logout', {}, { withCredentials: true }).pipe(
      tap(() => {
        this.jwt = undefined;
        this._user$.next(null);
      })
    );
  }

  getJwtToken() { return this.jwt; }

  // convenience example
  isAuthenticated(): boolean {
    return !!this.jwt || this.cookieBased; // if cookieBased we rely on server to validate
  }
}
