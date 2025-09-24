import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddJoueur } from '../model/joueurs/add-joueur';
import { UpdateRepasOfJoueurMatch } from '../model/update-repas-of-joueur-match';
import { JoueurFromMatchInfo } from '../model/joueur-from-match-info';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class JoueurMatchService {

  openSnackBar(message: string, action: string){ this._snackBar.open(message, action, { duration: 5000 })};
  
  constructor(
    private httpClient: HttpClient,
    private _snackBar: MatSnackBar) { }

  /**
   * ajoute un joueur au match OUVERT
   * @param joueur l'adresse mail doit correspondre à celle d'un des joueurs enregistrés
   */
  public addJoueurToMatch(joueur: AddJoueur): Promise<AddJoueur>{
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
      this.httpClient.post<AddJoueur>('api/joueurMatch', joueur, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: joueur => { resolve(joueur) },
        error: err => { this.openSnackBar(`Le joueur demandé n'a pas pu être ajouté au match. Si ce problème persiste veuillez contacter un administrateur.`, "OK")}
      })
    }) 
    
  }

  /**
   * Enlève un joueur du match OUVERT
   * @param id id du joueur
   */
  public removeJoueurToMatch(id: number) : Promise<void> {
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
      this.httpClient.delete<void>(`api/joueurMatch/${id}`, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: v => { resolve(v) },
        error: err => { this.openSnackBar(`Le joueur n'a pas pu être supprimé`, "OK")}
      })
    })
  }

  /**
   * @returns la liste des joueurs du match OUVERT
   */
  public getJoueursFromMatch(): Promise<Array<JoueurFromMatchInfo>>{
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
      this.httpClient.get<Array<JoueurFromMatchInfo>>('api/joueurMatch', { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next : (joueurs) => { resolve(joueurs) },
        error : err => { console.log(err), reject(err) }
      })
    })
  }

  public updateRepasOfJoueurMatch(updateRepas: UpdateRepasOfJoueurMatch): Promise<UpdateRepasOfJoueurMatch> {
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
      this.httpClient.put<UpdateRepasOfJoueurMatch>('api/JoueurMatch/changeRepas', updateRepas, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next : (joueurs) => { resolve(joueurs) },
        error : err => { console.log(err), reject(err) }
      })
    })
  }
}
