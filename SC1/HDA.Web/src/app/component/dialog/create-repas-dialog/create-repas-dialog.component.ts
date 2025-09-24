import { Component } from '@angular/core';
import { CreateRepas } from 'src/app/model/repas/create-repas';
import { RepasService } from 'src/app/service/repas.service';

@Component({
  selector: 'app-create-repas-dialog',
  templateUrl: './create-repas-dialog.component.html',
  styleUrls: ['./create-repas-dialog.component.scss']
})
export class CreateRepasDialogComponent {

  createRepas: CreateRepas = new CreateRepas()
  
}
