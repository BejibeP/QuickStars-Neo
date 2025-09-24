import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MailInfo } from '../model/mail-info';
import { MailDest } from '../model/mail-dest';

@Injectable({
  providedIn: 'root'
})
export class MailSoccerService {

  openSnackBar(message: string, action: string){ this._snackBar.open(message, action, { duration: 5000 })};

  constructor(
    private httpClient: HttpClient,
    private _snackBar: MatSnackBar) { }

  /**
   * Sauvegarde le mail sous format .msg
   * @param mailInfo objet et contenu du mail à envoyer
   */
  public sendMail(mailInfo: MailInfo): Promise<void> {
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => { 
      this.httpClient.post<MailInfo>('api/Mail', mailInfo, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: mail => { this.openSnackBar(`Le mail a bien été enregistré dans votre dossier de téléchargements.`, "OK"), resolve()},
        error: err => { this.openSnackBar(`La sauvegarde a échoué. Veuillez rééssayer plus tard.`, "OK") }
      })
    })
  }

    /**
   * Sauvegarde le mail sous format .msg
   * @param mailInfo objet et contenu du mail à envoyer
   */
    public sendMailJoueursMatch(mailInfo: MailInfo): Promise<void> {
      var token = sessionStorage.getItem('token');
      return new Promise((resolve, reject) => { 
        this.httpClient.post<MailInfo>('api/Mail/JoueursMatch', mailInfo, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
          next: mail => { this.openSnackBar(`Le mail a bien été enregistré dans votre dossier de téléchargements.`, "OK"), resolve()},
          error: err => { this.openSnackBar(`La sauvegarde a échoué. Veuillez rééssayer plus tard.`, "OK") }
        })
      })
    }

  /**
   * @returns objet contenant 2 string : liste des mails sopra et liste des mails externes
   */
  public getListMailsJoueurs(): Promise<MailDest> {
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => { 
      this.httpClient.get<MailDest>('api/Mail', { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: mail => { resolve(mail)},
        error: err => { this.openSnackBar(`La copie a échoué. Veuillez rééssayer plus tard.`, "OK") }
      })
    })
  }

  /**
   * @returns objet contenant 2 string : liste des mails sopra et liste des mails externes 
   * des joueurs INSCRITS au match OUVERT
   */
  public getListMailsMatchJoueurs(): Promise<MailDest> {
    var token = sessionStorage.getItem('token');
    return new Promise((resolve, reject) => { 
      this.httpClient.get<MailDest>('api/Mail/matchOuvert', { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: mail => { resolve(mail)},
        error: err => { this.openSnackBar(`La copie a échoué. Veuillez rééssayer plus tard.`, "OK") }
      })
    })
  }
    
}
