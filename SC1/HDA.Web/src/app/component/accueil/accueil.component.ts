import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ModifyProfil } from 'src/app/model/admin/modify-profil';
import { Register } from 'src/app/model/admin/register';
import { ResetPassword } from 'src/app/model/admin/reset-password';
import { Joueur } from 'src/app/model/joueurs/joueur';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { MenuComponent } from '../menu/menu.component';

@Component({
  selector: 'app-accueil',
  templateUrl: './accueil.component.html',
  styleUrls: ['./accueil.component.scss']
})
export class AccueilComponent implements OnInit {


  joueur: Joueur = new Joueur();
  reset: ResetPassword = new ResetPassword();
  newUser: Register = new Register();
  modify: ModifyProfil = new ModifyProfil();

  isConnected: boolean = false;
  openState1 = false; //pour le mat-expansion-panel
  openState2 = false;
  openState3 = false;
  emailPattern = '[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}'
  hide = true; //pour l'oeil de visibilité du mot de passe
  hide1 = true;
  hide2 = true;

  constructor(
    private authService: AuthenticationService,
    ){}

  ngOnInit(): void {
    this.authService.isConnectedPreview$.subscribe(b => 
      this.isConnected = b)
    this.authService.getConnectedUser().subscribe((name) => this.modify.userName = name);
    this.authService.getConnectedUserMail().subscribe((mail) => this.modify.mail = mail)
  }

  async modifyPassword(formModify: NgForm){
    await this.authService.resetPassword(this.reset);
    formModify.resetForm();
  }

  async newAdmin(formRegister: NgForm){
    await this.authService.register(this.newUser);
    formRegister.resetForm();
  }

  async modifyProfil(formModify: NgForm){
    await this.authService.modifyProfil(this.modify);
    formModify.resetForm();
  }
}
