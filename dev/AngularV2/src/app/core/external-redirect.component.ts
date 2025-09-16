import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExternalRedirectService } from './external-redirect.service';

@Component({
  selector: 'app-external-redirect',
  template: `<p>Redirection en cours…</p>`
})
export class ExternalRedirectComponent implements OnInit {
  constructor(private route: ActivatedRoute, private ext: ExternalRedirectService) {}

  ngOnInit(): void {
    const app = this.route.snapshot.paramMap.get('app')!;
    this.ext.getExternalRedirectUrl(app).subscribe({
      next: (url) => {
        // backend should validate and return a signed one-time URL
        window.location.href = url;
      },
      error: (err) => {
        console.error('Redirect failed', err);
        // show friendly UI or fallback
        alert('Impossible de rediriger vers l'application externe.');
      }
    });
  }
}
