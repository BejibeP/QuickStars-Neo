import { Component, ElementRef, EventEmitter, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { DeleteJoueurDialogComponent } from '../../dialog/delete-joueur-dialog/delete-joueur-dialog.component';
import { Membre } from 'src/app/model/membres/membre';
import { MemberService } from 'src/app/service/HDA/member.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.scss']
})
export class MembersComponent implements OnInit{

  membre: Membre = new Membre();
  membres!: Membre[];
  all!: number;
  
  openState = true; //pour le mat-expansion-panel
  openState2 = false;

  //initialisation du tableau
  displayedColumns: string[] = ['nom', 'prenom', 'mail', 'modify', 'supprimer']
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  dataSource = new MatTableDataSource(this.membres)

  constructor(private membreService: MemberService, private _snackBar: MatSnackBar, private _dialog: MatDialog) {}

  async ngOnInit(): Promise<void> {
    await this.chargerMembres();
  }

  async chargerMembres(){
    
    let membresList = await this.membreService.getMembres();

    //trie le tableau des joueurs dans l'ordre alphabétique selon le nom
    membresList.sort(
      (a: Membre, b: Membre) =>
        a.nom! < b.nom! ? -1 : 1
    );

    this.dataSource.data = membresList;
    this.all = membresList.length;
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getPageSizeOptions(){
    if (this.dataSource.data.length < 50 ){
      return [10, 50, 100]
    }
    return [10, 50, 100, this.dataSource.data.length]
  }

  async save(form: NgForm){
    await this.membreService.addMembre(this.membre).then((value) => {
      if (value != null) { 
        this.openSnackBar(`Le joueur ${this.membre.mail} a bien été ajouté à la liste des joueurs pouvant s'inscrire aux matchs`, "OK"); 
        form.resetForm();
      }
      else { this.openSnackBar(`L'email ${this.membre.mail} est déjà présent dans la liste.`, "OK"); }
    });
    await this.chargerMembres();
  }

  async modify(joueurModify: Membre){
    await this.membreService.updateMembre(joueurModify).then((v)=> {
      this.openSnackBar(`Le joueur a bien été mis à jour`, "OK");
    })
    await this.chargerMembres();
  }

  async suppr(id: number){
    const dialogRef = this._dialog.open(DeleteJoueurDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result == "true"){
        this.membreService.deleteMembre(id).then((v) => {
          this.openSnackBar(`Le joueur sélectionné a bien été supprimé`, "OK");
          this.chargerMembres();
        });
      }
    });
  }

  openSnackBar(message: string, action: string){
    this._snackBar.open(message, action, { duration: 5000 })
  };

}
