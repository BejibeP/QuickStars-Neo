import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <h1>Angular20 Skeleton</h1>
    <nav>
      <a routerLink="/">Home</a> |
      <a routerLink="/external/foo">Go to External App (foo)</a>
    </nav>
    <router-outlet></router-outlet>
  `
})
export class AppComponent {}
