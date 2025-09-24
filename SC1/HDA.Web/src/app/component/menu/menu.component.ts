import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ProfilModel } from 'src/app/model/admin/profil-model';
import { AuthenticationService } from 'src/app/service/authentication.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit{

  user!: string;

  constructor(private authSrv: AuthenticationService){
  }
  
  isConnected: boolean = false;
  WIP: boolean = false;
  isWIP: boolean = false;


  async ngOnInit(): Promise<void> {
    await this.checkConnectedUser() 
  }
  
  async logout(){
   this.authSrv.logout();
  }
  
  /**
   * Regarde si l'utilisateur est connecté et, si oui, récupère son nom
   */
  async checkConnectedUser(){
    this.authSrv.isConnectedPreview$.subscribe(b => {
      this.isConnected = b;
      this.isWIP = b && this.WIP;
      if (b === true){
        this.authSrv.getConnectedUser().subscribe(u => this.user = u);
      }
    })
  }
}
