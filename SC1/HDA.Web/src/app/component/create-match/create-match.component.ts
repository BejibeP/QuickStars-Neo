import { Component, OnInit } from '@angular/core';
import { CreateMatch } from 'src/app/model/match/create-match';
import { Match } from 'src/app/model/match/match';
import { MatchService } from 'src/app/service/match.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { CreateMatchDialogComponent } from '../dialog/create-match-dialog/create-match-dialog.component';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { DeleteMatchDialogComponent } from '../dialog/delete-match-dialog/delete-match-dialog.component';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-create-match',
  templateUrl: './create-match.component.html',
  styleUrls: ['./create-match.component.scss']
})
export class CreateMatchComponent implements OnInit {
  user!: string;
  match: CreateMatch = new CreateMatch();
  matchOuvert!: Match;
  matchsDataSource!: Match[]
  
  openSnackBar(message: string, action: string){ this._snackBar.open(message, action, { duration: 5000 })};
  displayedColumns: string[] = ['libelle', 'createur', 'ouvrir', 'delete']
  openState = true; //pour le mat-expansion-panel

  constructor(
      private matchService: MatchService,
      private _snackBar: MatSnackBar,
      private _dialog: MatDialog,
      private authsrv: AuthenticationService
  ){}

  /**
   * Récupère le nom de l'utilisateur connecté, le match ouvert et les matchs fermés
   */
  async ngOnInit(): Promise<void> {
    this.authsrv.getConnectedUser().subscribe(u => this.user = u)
    this.matchOuvert = await this.matchService.getOpenMatch()
    this.matchsDataSource = await this.matchService.getAllClosedMatchs();
  }

  /**
   * Ouvre une boîte de dialog indiquant que le match ouvert fermera le précédent match ouvert,
   * si on clique sur "oui" le match sera sauvegardé avec la fonction "save()"
   */
  askCreateMatchOuvert(form: NgForm): void {
    const dialogRef = this._dialog.open(CreateMatchDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result == "true"){
        this.save(true, form)
      }
    });
  }

/**
 * sauvegarde le nouveau match et actualise les valeurs affichées sur la page (matchs ouvert/fermés)
 * @param b : true = le match sera ouvert, false = le match sera fermé
 * @param form : pour réinitialiser les valeurs du formulaire à la sauvegarde
 */
  async save(b : boolean, form: NgForm): Promise<void>{
    this.match.inscriptionsOuvertes = b;

    let matchCree = await this.matchService.createMatch(this.match);

    form.resetForm();

    this.matchOuvert = await this.matchService.getOpenMatch();
    this.matchsDataSource = await this.matchService.getAllClosedMatchs();
    this.openSnackBar(`Le match "${matchCree.libelle}" a bien été ajouté à la liste`, "OK")
  }

  async askOpenMatch(id: number){
    const dialogRef = this._dialog.open(CreateMatchDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result == "true"){
        this.open(id)
      }
    })
  }

  /**
   * ouvre un match fermé, snackbar pour l'indiquer, et actualise les valeurs de matchs ouvert/fermés
   * @param id : id du match
   */
  async open(id: number): Promise<void> {
    let matchO = await this.matchService.openMatch(id);

    this.matchOuvert = await this.matchService.getOpenMatch();
    this.openSnackBar(`Le match du "${matchO.libelle}" est maintenant ouvert à l'inscription`, "OK");
    this.matchsDataSource = await this.matchService.getAllClosedMatchs()
  }

  /**
   * Snackbar demande la suppression du match : si clique sur oui alors 
   * renvoie vers fonction delMatch(id)
   * @param id id du match
   */
  askDeleteMatch(id: number){
    const dialogRef = this._dialog.open(DeleteMatchDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result == "true"){
        this.delMatch(id)
      }
    });
  }

  /**
   * supprime un match et actualise les valeurs
   * @param id : id du match à supprimer
   */
  async delMatch(id: number): Promise<void> {
    await this.matchService.deleteMatch(id);
    this.openSnackBar(`Le match sélectionné a bien été supprimé`, "OK");

    this.matchOuvert = await this.matchService.getOpenMatch();
    this.matchsDataSource = await this.matchService.getAllClosedMatchs();
  }




}
