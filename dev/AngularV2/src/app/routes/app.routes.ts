import { Routes } from '@angular/router';
import { ExternalRedirectComponent } from '../core/external-redirect.component';
import { DummyHomeComponent } from '../shared/dummy-home.component';
import { AuthGuard } from '../core/auth.guard';

export const routes: Routes = [
  { path: 'profile', loadChildren: () => import('../features/profile/profile.module').then(m => m.ProfileModule) },
  { path: '', component: DummyHomeComponent },
  { path: 'external/:app', component: ExternalRedirectComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
];
