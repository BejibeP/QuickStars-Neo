import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NewJoueur } from '../model/joueurs/new-joueur';
import { Observable } from 'rxjs';
import { Joueur } from '../model/joueurs/joueur';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AddListJoueurs } from '../model/joueurs/add-list-joueurs';

@Injectable({
  providedIn: 'root'
})
export class JoueurService {

  openSnackBar(message: string, action: string){ this._snackBar.open(message, action, { duration: 5000 })};

  constructor(
    private httpClient: HttpClient,
    private _snackBar: MatSnackBar) { }

    /**
     * Ajoute un nouveau joueur à la liste des joueurs pouvant s'inscrire aux matchs
     */
  public addNewJoueur(joueur: NewJoueur): Promise<NewJoueur>{
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => { 
      this.httpClient.post<NewJoueur>('api/Joueur', joueur, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: joueur => { resolve(joueur)},
        error: err => { }
      })
    })
  }

    /**
   * Ajoute un ou plusieurs nouveaux joueurs à la liste des joueurs pouvant s'inscrire aux matchs
   */
    public addListNewJoueur(listJoueurs: AddListJoueurs): Promise<AddListJoueurs>{
      var token = sessionStorage.getItem('token');
      return new Promise((resolve, reject) => { 
        this.httpClient.post<AddListJoueurs>('api/Joueur/List', listJoueurs, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
          next: v => { this.openSnackBar(`Les joueurs ont bien été ajoutés à la liste des joueurs pouvant s'inscrire aux matchs`, "OK"), resolve(v)},
          error: err => { this.openSnackBar(`Les joueurs n'ont pas pu être ajoutés à la liste des joueurs pouvant s'inscrire aux matchs`, "OK") }
        })
      })
    }

  /**
   * Récupère l'ensemble des joueurs
   */
  public async getAllJoueurs(): Promise<Array<Joueur>>{
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => { 
          this.httpClient.get<Array<Joueur>>('api/Joueur/All', { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe((joueurs) => { 
            resolve(joueurs) }) 
        })
  }

  /**
   * Supprime un joueur : il ne pourra plus s'inscrire aux matchs
   */
  public deleteJoueur(id: number) {
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
      this.httpClient.delete<void>(`api/Joueur/${id}`, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: (v) => {resolve(v)},
        error: err => { this.openSnackBar(`Le joueur demandé n'a pas pu être supprimé de la liste`, "OK") }
      })
    })
  }

  public updateJoueur(joueur: Joueur): Promise<void>{
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
      this.httpClient.put<void>(`api/Joueur`, joueur, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: (v) => {resolve(v)},
        error: err => { this.openSnackBar(`Le joueur demandé n'a pas pu être mis à jour`, "OK") }
      })
    })
  }
}
