import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Login } from '../model/admin/login';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TokenString } from '../model/admin/token-string';
import { BehaviorSubject, Observable } from 'rxjs';
import { ResetPassword } from '../model/admin/reset-password';
import { Register } from '../model/admin/register';
import { ModifyProfil } from '../model/admin/modify-profil';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  isConnectedPreview$!: Observable<boolean>;
  openSnackBar(message: string, action: string){
    this._snackBar.open(message, action, { duration: 5000 })
  };

  private isConnectedPrev!: BehaviorSubject<boolean>;

  constructor(
    private httpClient: HttpClient, 
    private router: Router,
    private _snackBar: MatSnackBar,
    ) {
      this.isConnectedPrev = new BehaviorSubject<boolean>(sessionStorage.getItem('token') ? true : false);
      this.isConnectedPreview$ = this.isConnectedPrev.asObservable();
    };

    /**
     * Connexion de l'administrateur
     * @param login nom d'utilisateur et mot de passe
     */
  public async login(login: Login): Promise<string> {
    var response = new Promise<string>((resolve, reject) => {
      this.httpClient.post<TokenString>('api/Authentication/login', login).subscribe({
        next: (ts) => { 
          sessionStorage.setItem('token', `${ts.token}`)
          this.openSnackBar("Vous êtes maintenant connecté en tant qu'administrateur", "OK");
          resolve("Vous êtes maintenant connecté en tant qu'administrateur") 
          this.isConnectedPrev.next(sessionStorage.getItem('token') ? true : false);
        },
        error: (err) => { 
          this.openSnackBar(`Le nom d'utilisateur et/ou le mot de passe est incorrect`, "OK")
          reject(`Le nom d'utilisateur et/ou le mot de passe est incorrect, erreur ${err.status}`)}
      })
    })
    return response
  }

  /**
   * @returns nom de l'utilisateur
   */
  public getConnectedUser(): Observable<string>  {
    var token = sessionStorage.getItem('token');
    return this.httpClient.get('api/Authentication/connected', { headers: new HttpHeaders({ Authorization: `Bearer ${token}`}), responseType:'text'});
  }

    /**
   * @returns mail de l'utilisateur
   */
    public getConnectedUserMail(): Observable<string>  {
      var token = sessionStorage.getItem('token');
      return this.httpClient.get('api/Authentication/connectedMail', { headers: new HttpHeaders({ Authorization: `Bearer ${token}`}), responseType:'text'});
    }

  public async logout (){
    sessionStorage.removeItem('token')
    this.isConnectedPrev.next(sessionStorage.getItem('token') ? true : false)
    this.router.navigateByUrl('/match')
  }

  public resetPassword(reset: ResetPassword){
    var token = sessionStorage.getItem('token');
    var response = new Promise<string>((resolve, reject) => {
      this.httpClient.post<ResetPassword>('api/Authentication/resetPassword', reset, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: (ts) => { 
          this.openSnackBar("Votre mot de passe a bien été mis à jour", "OK");
          resolve("mot de passe mis à jour") 
        },
        error: (err) => { 
          this.openSnackBar(`La mise a jour du mot de passe a échoué, veuillez rééssayer`, "OK")
          reject(`Le nom d'utilisateur et/ou le mot de passe est incorrect, erreur ${err.status}`)}
      })
    })
    return response
  }

  /**
   * Enregistre un nouvel administrateur
   * @param newUser informations du nouvel utilisateur
   */
  public register(newUser: Register){
    var token = sessionStorage.getItem('token');
    var response = new Promise<string>((resolve, reject) => {
      this.httpClient.post('api/Authentication/register', newUser, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
        next: (ts) => { 
          this.openSnackBar("Le nouvel utilisateur a bien été créé", "OK");
          resolve("utilisateur créé") 
        },
        error: (err) => { 
          this.openSnackBar(`L'ajout d'un nouvel utilisateur a échoué, veuillez rééssayer`, "OK")
          reject(`erreur ${err.status}`)}
      })
    })
    return response
  }

  /**
   * Modifie les informations du profil
   */
    public modifyProfil(modif: ModifyProfil){
      var token = sessionStorage.getItem('token');
      var response = new Promise<string>((resolve, reject) => {
        this.httpClient.put<ModifyProfil>('api/Authentication/modifyProfil', modif, { headers: new HttpHeaders({ Authorization: `Bearer ${token}`})}).subscribe({
          next: (ts) => { 
            this.openSnackBar("Les informations utilisateur ont bien été mises à jour", "OK");
            resolve("utilisateur mis à jour") 
          },
          error: (err) => { 
            this.openSnackBar(`La mise à jour de l'utilisateur à échoué, veuillez rééssayer`, "OK")
            reject(`erreur ${err.status}`)}
        })
      })
      return response
    }
}

