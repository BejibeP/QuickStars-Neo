import { Component, ElementRef, EventEmitter, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { Joueur } from 'src/app/model/joueurs/joueur';
import { NewJoueur } from 'src/app/model/joueurs/new-joueur';
import { JoueurService } from 'src/app/service/joueur.service';
import { CopieMailJoueursDialogComponent } from '../../dialog/copie-mail-joueurs-dialog/copie-mail-joueurs-dialog.component';
import { DeleteJoueurDialogComponent } from '../../dialog/delete-joueur-dialog/delete-joueur-dialog.component';
import { ImportMailsJoueursService } from 'src/app/service/import-mails-joueurs.service';
import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { AddListJoueurs } from 'src/app/model/joueurs/add-list-joueurs';

@Component({
  selector: 'app-bookings',
  templateUrl: './bookings.component.html',
  styleUrls: ['./bookings.component.scss']
})
export class BookingsComponent implements OnInit{
  
  joueur: NewJoueur = new NewJoueur();
  joueurs!: Joueur[];
  all!: number;
  fileName!: string;
  listJoueurs: AddListJoueurs = new AddListJoueurs();

  openSnackBar(message: string, action: string){
    this._snackBar.open(message, action, { duration: 5000 })
  };
  openState = true; //pour le mat-expansion-panel
  openState2 = false;

  //initialisation du tableau
  displayedColumns: string[] = ['nom', 'prenom', 'mail', 'dateAjout', 'modify', 'supprimer']
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  dataSource = new MatTableDataSource(this.joueurs)

  constructor(
    private joueurService: JoueurService,
    private _snackBar: MatSnackBar,
    private _dialog: MatDialog,
    private elementRef: ElementRef,
    private importMail: ImportMailsJoueursService,
    private ren: Renderer2
  ){}

  async ngOnInit(): Promise<void> {
    await this.chargeTableauJoueurs();
  }

  /**
   * ajoute un joueur à la liste et recharge le tableau
   */
  async save(form: NgForm){
    await this.joueurService.addNewJoueur(this.joueur).then((value) => {
      if (value != null) { 
        this.openSnackBar(`Le joueur ${this.joueur.mail} a bien été ajouté à la liste des joueurs pouvant s'inscrire aux matchs`, "OK"); 
        form.resetForm();
      }
      else { this.openSnackBar(`L'email ${this.joueur.mail} est déjà présent dans la liste.`, "OK"); }
    });
    
    await this.chargeTableauJoueurs();
  }

    /**
   * ajoute plusieurs joueurs à la liste et recharge le tableau
   */
    async saveListJoueurs(form: NgForm){
      await this.joueurService.addListNewJoueur(this.listJoueurs).then((value) => {
          form.resetForm();
      });
      
      await this.chargeTableauJoueurs();
    }

  /**
   * Modifie le joueur, rétablie la valeur de wantModify à false
   * @param joueurModify joueur à modifier
   */
  async modify(joueurModify: Joueur){
    await this.joueurService.updateJoueur(joueurModify).then((v)=> {
      this.openSnackBar(`Le joueur a bien été mis à jour`, "OK") 
      joueurModify.wantModify = false;
    })
    await this.chargeTableauJoueurs();
  }

    /**
   * Demande à modifier un joueur: met la valeur de wantModify à true
   */
    async askModify(joueur: Joueur){
      joueur.wantModify = true;
    }

  /**
   * ouvre une boîte de dialog pour demander de confirmer 
   * l'envie de supprimer le joueur: le supprime si la réponse est "oui"
   * @param id id du joueur
   */
  async suppr(id: number){
    const dialogRef = this._dialog.open(DeleteJoueurDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result == "true"){
        this.joueurService.deleteJoueur(id).then((v) => {
          this.openSnackBar(`Le joueur sélectionné a bien été supprimé`, "OK");
          this.chargeTableauJoueurs();
        });
      }
    });
  }

  /**
   * charge le tableau des joueurs et l'ensemble des données
   */
  async chargeTableauJoueurs(){
    let joueursList = await this.joueurService.getAllJoueurs();
    //trie le tableau des joueurs dans l'ordre alphabétique selon le nom
    joueursList.sort(
      (a: Joueur, b: Joueur) =>
        a.nom! < b.nom! ? -1 : 1
    );
    this.dataSource.data = joueursList;
    this.all = joueursList.length;
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    // //enlève l'info "items per page" dans le paginator
    // const itemsPerPage = this.elementRef.nativeElement.querySelector(
    //   '.mat-mdc-paginator-page-size');
    // this.ren.setStyle(itemsPerPage, 'display', 'none');
  }

  getPageSizeOptions(){
    if (this.dataSource.data.length < 50 ){
      return [10, 50, 100]
    }

    return [10, 50, 100, this.dataSource.data.length]
  }

  /**
   * ouvre une boîte de dialog qui permet de copier la liste des mails des joueurs
   */
  copieMail(){
    const dialogRef = this._dialog.open(CopieMailJoueursDialogComponent);

    dialogRef.afterClosed().subscribe(result => { });
  }

}
