import { Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Repas } from 'src/app/model/repas/repas';
import { RepasService } from 'src/app/service/repas.service';
import { DeleteRepasDialogComponent } from '../dialog/delete-repas-dialog/delete-repas-dialog.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { NgForm } from '@angular/forms';
import { CreateRepas } from 'src/app/model/repas/create-repas';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-repas',
  templateUrl: './repas.component.html',
  styleUrls: ['./repas.component.scss']
})
export class RepasComponent implements OnInit{

  repasVue!: Repas[];
  createRepas: CreateRepas = new CreateRepas()
  all!: number;
  openState = false; //pour le mat-expansion-panel
  openSnackBar(message: string, action: string){
    this._snackBar.open(message, action, { duration: 5000 })
  };

  //initialisation pour le tableau de valeurs
  displayedColumns: string[] = ['formule', 'dispo', 'buttonModif', 'buttonDel'];
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  dataSource = new MatTableDataSource(this.repasVue);

  constructor(
    private repasService: RepasService,
    private _dialog: MatDialog,
    private _snackBar: MatSnackBar,
    private elementRef: ElementRef,
    private ren: Renderer2
  ){}

  async ngOnInit(): Promise<void> {
    await this.chargeTableauJoueurs();
  }

  /**
   * charge le tableau des repas
   */
    async chargeTableauJoueurs(){
      this.repasVue = await this.repasService.getAllRepas();
      this.dataSource.data = this.repasVue;
      this.all = this.repasVue.length;
      this.dataSource.paginator = this.paginator;
  
      const itemsPerPage = this.elementRef.nativeElement.querySelector(
        '.mat-mdc-paginator-page-size');
      this.ren.setStyle(itemsPerPage, 'display', 'none');
    }

  async create(form: NgForm){
    await this.repasService.createRepas(this.createRepas).then((value) => {
        this.openSnackBar(`Le joueur ${this.createRepas} a bien été ajouté à la liste des repas`, "OK"); 
        form.resetForm();
      });
    await this.chargeTableauJoueurs();
  }

  /**
   * Enregistre la modification du repas, reset la valeur de wantModify a false
   * @param repas : repas à modifier
   * @param id : id du repas à modifier
   */
  async modify(repas: Repas, id: number){
    await this.repasService.updateRepas(repas, id);
    this.repasVue = await this.repasService.getAllRepas();
    repas.wantModify = false;
  }


  /**
   * Change la valeur de wantModify: permettant de modifier ou non un repas
   */
  async askModify(repas: Repas){
    if (repas.wantModify === true) {
      repas.wantModify = false;
    }
    else {
      repas.wantModify = true;
    }
  }

  /**
   * Ouvre une boîte de dialogue demandant de confirmer la volonté de supprimer le repas
   */
  askDeleteRepas(id: number){
    const dialogRef = this._dialog.open(DeleteRepasDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result == "true"){
        this.suppr(id)
      }
    });
  }

  
  async suppr(id: number){
    await this.repasService.deleteRepas(id);
    await this.chargeTableauJoueurs();
  }
}
