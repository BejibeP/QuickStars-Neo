import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Membre } from 'src/app/model/membres/membre';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  constructor(private httpClient: HttpClient, private _snackBar: MatSnackBar) { }

  public async getMembres(): Promise<Array<Membre>>{
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => 
    { 
      this.httpClient.get<Array<Membre>>('api/Membre/Zero', { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe((joueurs) => { 
        resolve(joueurs) }) 
    });
  }

  public addMembre(membre: Membre): Promise<Membre>{
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => { 
      this.httpClient.post<Membre>('api/Membre', membre, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: joueur => { resolve(joueur)},
        error: err => { }
      })
    })
  }

  public updateMembre(membre: Membre): Promise<void>{
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
      this.httpClient.put<void>(`api/Membre`, membre, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: (v) => {resolve(v)},
        error: err => { this.openSnackBar(`Le joueur demandé n'a pas pu être mis à jour`, "OK") }
      })
    })
  }

  public deleteMembre(id: number) {
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
      this.httpClient.delete<void>(`api/Membre/${id}`, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: (v) => {resolve(v)},
        error: err => { this.openSnackBar(`Le joueur demandé n'a pas pu être supprimé de la liste`, "OK") }
      })
    })
  }

  openSnackBar(message: string, action: string){ this._snackBar.open(message, action, { duration: 5000 })};

}
