import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ExternalRedirectService {
  constructor(private http: HttpClient) {}

  // request a backend-created one-time token and redirect url
  getExternalRedirectUrl(appIdentifier: string): Observable<string> {
    return this.http.post<{ url: string }>(`/api/external/create-redirect`, { app: appIdentifier })
      .pipe(map(r => r.url));
  }

  // alternative: POST form method helper
  doFormPost(url: string, data: Record<string,string>) {
    const form = document.createElement('form');
    form.method = 'POST';
    form.action = url;
    Object.entries(data).forEach(([k, v]) => {
      const input = document.createElement('input');
      input.type = 'hidden';
      input.name = k;
      input.value = v;
      form.appendChild(input);
    });
    document.body.appendChild(form);
    form.submit();
  }
}
