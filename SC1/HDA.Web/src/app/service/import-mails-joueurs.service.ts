import { HttpClient, HttpEventType, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImportMailsJoueursService {

  constructor(private httpClient: HttpClient, ) { }

  public readMail(file: FormData){
    var token = sessionStorage.getItem('token');
    return this.httpClient.post('api/ImportMailsJoueurs', file, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})})
  }
}
