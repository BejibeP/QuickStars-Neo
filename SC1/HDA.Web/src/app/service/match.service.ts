import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateMatch } from '../model/match/create-match';
import { Observable } from 'rxjs';
import { Match } from '../model/match/match';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  openSnackBar(message: string, action: string){ this._snackBar.open(message, action, { duration: 5000 })};
  private token!: string | null

  constructor(
    private httpClient: HttpClient,
    private _snackBar: MatSnackBar
    ) {}

  public async createMatch(match: CreateMatch): Promise<CreateMatch>{
    this.token = sessionStorage.getItem('token') 
    return new Promise((resolve) => {
      this.httpClient.post<CreateMatch>('api/Match', match, { headers: new HttpHeaders({ Authorization: `Bearer ${this.token}`})} )
      .subscribe({
        next: match => resolve(match),
        error: err => { this.openSnackBar(`Le match demandé n'a pas pu être ajouté`, "OK") }
      })
    })
  }

  public async getAllClosedMatchs(): Promise<Array<Match>>{
    this.token = sessionStorage.getItem('token') 
    return new Promise((resolve) => { 
      this.httpClient.get<Array<Match>>('api/Match/MatchsFermes', { headers: new HttpHeaders({ Authorization: `Bearer ${this.token}`})})
      .subscribe(matchs => resolve(matchs))
    })
  }

  public async getOpenMatch(): Promise<Match>{
    return new Promise((resolve) => {
      this.httpClient.get<Match>('api/Match/MatchOuvert')
      .subscribe(match => resolve(match))
    })
  }

  public async openMatch(id: number): Promise<Match>
  {
    this.token = sessionStorage.getItem('token') 
    return new Promise((resolve) => {
      this.httpClient.put<Match>('api/Match/OuvreMatch', id, { headers: new HttpHeaders({ Authorization: `Bearer ${this.token}`})})
      .subscribe({
        next: match => resolve(match),
        error: err => { this.openSnackBar(`Le match demandé n'a pas pu être ouvert`, "OK") }
      })
    })
  }

  public deleteMatch(id: number): Promise<void> 
  {
    this.token = sessionStorage.getItem('token') 
    return new Promise((resolve, reject) => {
      this.httpClient.delete<void>(`api/Match/${id}`, { headers: new HttpHeaders({ Authorization: `Bearer ${this.token}`})})
      .subscribe({
        next: v => resolve(v),
        error: err => { this.openSnackBar(`Le match demandé n'a pas pu être supprimé`, "OK") }
      })
    })
  }
}
