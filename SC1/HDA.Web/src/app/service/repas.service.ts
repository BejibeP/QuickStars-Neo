import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateRepas } from '../model/repas/create-repas';
import { Repas } from '../model/repas/repas';
import { Observable } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class RepasService {

  openSnackBar(message: string, action: string){ this._snackBar.open(message, action, { duration: 5000 })}; 

  constructor(
    private httpClient: HttpClient,
    private _snackBar: MatSnackBar
    ) { }

  public createRepas(repas: CreateRepas): Promise<Repas>{
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
    this.httpClient.post<Repas>('api/FormuleRepas', repas, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
      next: (repas) => { resolve(repas) },
      error: err => this.openSnackBar(`Le repas demandé n'a pas pu être ajouté`, "OK")
      }) 
    })
    }

  public getAllRepas(): Promise<Array<Repas>>{
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
      this.httpClient.get<Array<Repas>>('api/FormuleRepas', { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe((repas) => { 
      resolve(repas) }) 
    })
  }

  public getAllAvailableRepas(): Promise<Array<Repas>>{
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
      this.httpClient.get<Array<Repas>>('api/FormuleRepas/Dispo').subscribe((repas) => { 
      resolve(repas) }) 
    })
  }

  public updateRepas(repas: Repas, id: number): Promise<Repas>
  {
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
    this.httpClient.put<Repas>(`api/FormuleRepas/${id}`, repas, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
      next: (repas) => { this.openSnackBar(`Le repas a bien été modifié`, "OK"), resolve(repas) },
      error: err => this.openSnackBar(`Le repas demandé n'a pas pu être modifié`, "OK")
    }) 
    })
  }

  public deleteRepas(id: number): Promise<void> 
  {
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => {
      this.httpClient.delete<void>(`api/FormuleRepas/${id}`, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: (v) => resolve(v),
        error: err => this.openSnackBar(`Le repas demandé n'a pas pu être supprimé`, "OK")
      })
      })
  }

}
