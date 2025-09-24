import { Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
import { AddJoueur } from 'src/app/model/joueurs/add-joueur';
import { JoueurFromMatchInfo } from 'src/app/model/joueur-from-match-info';
import { JoueurMatchService } from 'src/app/service/joueur-match.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatchService } from 'src/app/service/match.service';
import { Match } from 'src/app/model/match/match';
import { RepasService } from 'src/app/service/repas.service';
import { Repas } from 'src/app/model/repas/repas';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { MailInvitationDialogComponent } from '../dialog/mail-invitation-dialog/mail-invitation-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MailParticipantsDialogComponent } from '../dialog/mail-participants-dialog/mail-participants-dialog.component';
import { ModifyRepasDialogComponent } from '../dialog/modify-repas-dialog/modify-repas-dialog.component';
import { UpdateRepasOfJoueurMatch } from 'src/app/model/update-repas-of-joueur-match';
import { NgForm } from '@angular/forms';
import { MailSoccerService } from 'src/app/service/mail-soccer.service';
import { MailDest } from 'src/app/model/mail-dest';
import { CopieMailJoueursMatchDialogComponent } from '../dialog/copie-mail-joueurs-match-dialog/copie-mail-joueurs-match-dialog.component';
import { ManqueJoueursDialogComponent } from '../dialog/manque-joueurs-dialog/manque-joueurs-dialog.component';
import { CopieMailJoueursDialogComponent } from '../dialog/copie-mail-joueurs-dialog/copie-mail-joueurs-dialog.component';

@Component({
  selector: 'app-joueur-match',
  templateUrl: './joueur-match.component.html',
  styleUrls: ['./joueur-match.component.scss']
})
export class JoueurMatchComponent implements OnInit {

  joueur: AddJoueur = new  AddJoueur();
  joueursVue!: JoueurFromMatchInfo[];
  repasVue!: Repas[];
  matchOuvert!: Match;

  all!: number;
  wantRepas = false;
  isConnected: boolean = false;
  terrainComplet: boolean = false;

  openSnackBar(message: string, action: string){ this._snackBar.open(message, action, { duration: 5000 })};

  //initialisation pour le tableau de valeurs
  displayedColumns: string[] = ['nom', 'prenom', 'heureDeReponse', 'formuleRepas', 'buttonDel'];
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  dataSource = new MatTableDataSource(this.joueursVue);
  
  openState = true; //pour le mat-expansion-panel

  constructor(
      private joueurMatchService: JoueurMatchService,
      private authService: AuthenticationService,
      private matchService: MatchService,
      private repasService: RepasService,
      private _snackBar: MatSnackBar,
      private _dialog: MatDialog,
      private elementRef: ElementRef,
      private ren: Renderer2
    ){}


  /**
   * Initialise les valeurs du tableau
   */
  async ngOnInit(): Promise<void> {
    this.authService.isConnectedPreview$.subscribe(b => 
      this.isConnected = b)
    this.matchOuvert = await this.matchService.getOpenMatch();
    this.repasVue = await this.repasService.getAllAvailableRepas();

    this.joueurMatchService.getJoueursFromMatch().then(j => {
      this.dataSource.data = j,
      this.all = j.length;
      this.dataSource.paginator = this.paginator,
      this.dataSource.sort = this.sort
    });

    // //enlève l'info "items per page" dans le paginator
    // const itemsPerPage = this.elementRef.nativeElement.querySelector(
    //   '.mat-mdc-paginator-page-size');
    // this.ren.setStyle(itemsPerPage, 'display', 'none');
    
    this.dataSource = new MatTableDataSource(this.joueursVue);
    if (this.isConnected) {this.openState = false}
  }

  ngAfterViewInit(): void{
    this.dataSource.sort = this.sort
  }

  /**
   * récupère la liste des joueurs du match et instancie le tableau
   */
    async getJoueursFromMatch(): Promise<void> {
      this.dataSource.data = await this.joueurMatchService.getJoueursFromMatch();
      this.all = this.dataSource.data.length;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }


  /**
   * Ajouter un joueur au match
   * @param form formulaire d'ajout de joueur au match
   */
  async save(form: NgForm): Promise<void>{
    //ne pas garder la valeur du repas si la case n'est pas cochée
    if (!this.wantRepas) {this.joueur.formuleRepas = ""}
    await this.joueurMatchService.addJoueurToMatch(this.joueur).then((value) => {
      if (value != null) { 
        this.openSnackBar(`${this.joueur.mail} a bien été ajouté à la liste`, "OK"); 
        form.resetForm();
      }
      else { this.openSnackBar(`${this.joueur.mail} est déjà inscrit à ce match. Si vous souhaitez changer votre repas, veuillez contacter un administrateur.`, "OK");
     }
    });
    await this.getJoueursFromMatch();
    
  }

  //change la valeur de "want repas" à chaque click sur la checkbox
  wantRepasModify(){
    if (this.wantRepas === true) {
      this.wantRepas  = false;
    }
    else {
      this.wantRepas  = true;
    }
  }

  /**
   * Demande à modifier le repas: change la valeur de wantModify et permet d'afficher un select pour sélectionner un autre repas
   * @param joueurModify joueur dont le repas est à modifier
   */
  async askModify(joueurModify: JoueurFromMatchInfo){
    if (joueurModify.wantModify === true) {
      let updateRepas = new UpdateRepasOfJoueurMatch();
      updateRepas.idJoueur = joueurModify.idJoueur;
      updateRepas.formuleRepas = joueurModify.formuleRepas;
      await this.joueurMatchService.updateRepasOfJoueurMatch(updateRepas).then((value) => {
        this.openSnackBar(`Le repas de ${joueurModify.nom} ${joueurModify.prenom} a bien été changé.`, "OK")
      });
      this.repasVue = await this.repasService.getAllRepas();
      joueurModify.wantModify = false;
    }
    else {
      joueurModify.wantModify = true;
    }
  }

  /**
   * Enlève un joueur du match OUVERT
   * @param id id du joueur
   */
  async supprimer(id: number): Promise<void>{
    await this.joueurMatchService.removeJoueurToMatch(id);
    await this.getJoueursFromMatch();
    this.openSnackBar(`Le joueur a bien été supprimé de la liste`, "OK");
  }

  /**
   * ouvre une boîte de dialog avec le html du mail d'invitation personnalisé
   */
  mailInvitationDialog(){
    const dialogRef = this._dialog.open(MailInvitationDialogComponent);

    dialogRef.afterClosed().subscribe(result => { });
  }

  /**
   * ouvre une boîte de dialog avec le html du mail de participation personnalisé
   */
  mailParticipantsDialog(){
    const dialogRef = this._dialog.open(MailParticipantsDialogComponent);

    dialogRef.afterClosed().subscribe(result => {});
  }

    /**
   * ouvre une boîte de dialog avec le html du mail d'appel à d'autres joueurs
   */
  mailManqueJoueursDialog(){
    const dialogRef = this._dialog.open(ManqueJoueursDialogComponent);

    dialogRef.afterClosed().subscribe(result => {});
  }

  /**
 * Liste des mails des joueurs
 */
  copieMailsInscrit(){
    const dialogRef = this._dialog.open(CopieMailJoueursMatchDialogComponent);

    dialogRef.afterClosed().subscribe(result => { });
  }
  
  /**
   * ouvre une boîte de dialog qui permet de copier la liste des mails des joueurs
   */
  copieMailsTous(){
    const dialogRef = this._dialog.open(CopieMailJoueursDialogComponent);

    dialogRef.afterClosed().subscribe(result => { });
  }

  getPageSizeOptions(){
    if (this.dataSource.data.length < 50 ){
      return [10, 20, 30, 40, 50]
    }
    return [10, 20, 30, 40, 50, this.dataSource.data.length]
  }

  checkTerrainComplet(){
    if (this.all%10 === 0) return true;
    return false;
  }

}

