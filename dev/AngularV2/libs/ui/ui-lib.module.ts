import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonModule as PrimeButtonModule } from 'primeng/button';
import { MatButtonModule } from '@angular/material/button';
import { MyButtonComponent } from './button/my-button.component';

@NgModule({
  declarations: [MyButtonComponent],
  imports: [CommonModule, PrimeButtonModule, MatButtonModule],
  exports: [MyButtonComponent, PrimeButtonModule, MatButtonModule]
})
export class UiLibModule {}
