import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExternalRedirectComponent } from './external-redirect.component';
import { AuthService } from './auth.service';
import { AuthGuard } from './auth.guard';

@NgModule({
  declarations: [ExternalRedirectComponent],
  imports: [CommonModule],
  providers: [AuthService, AuthGuard],
  exports: [ExternalRedirectComponent]
})
export class CoreModule {}
