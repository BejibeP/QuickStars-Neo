import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Match } from 'src/app/model/match/match';
import { Repas } from 'src/app/model/repas/repas';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { JoueurMatchService } from 'src/app/service/joueur-match.service';
import { MatchService } from 'src/app/service/match.service';
import { RepasService } from 'src/app/service/repas.service';
import { Pipe, PipeTransform } from '@angular/core';
import { MailSoccerService } from 'src/app/service/mail-soccer.service';
import { MailInfo } from 'src/app/model/mail-info';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-mail-invitation-dialog',
  templateUrl: './mail-invitation-dialog.component.html',
  styleUrls: ['./mail-invitation-dialog.component.scss']
})
export class MailInvitationDialogComponent implements OnInit {

  @ViewChild('toCopy') toCopy!: ElementRef;
 
  btnStyleCopie = 'btn-default';
  btnStyleSave = 'btn-default'
  matchOuvert!: Match;
  user!: string;
  repasVue?: Repas[];
  copie: boolean = false;
  save: boolean = false;
  path?: string;
  currentRoute!: string;

  constructor(
    private matchService: MatchService,
    private authenticationService: AuthenticationService,
    private repasService: RepasService,
    private mailService: MailSoccerService,
    ){  }

  async ngOnInit(): Promise<void> {
    this.matchOuvert = await this.matchService.getOpenMatch();
    this.authenticationService.getConnectedUser().subscribe(u => this.user = u);
    this.repasVue = await this.repasService.getAllAvailableRepas();
    this.currentRoute = window.location.href;
  }

  // change l'écriture du bouton une fois copié
  copierContenu(){
    if (this.copie){
      return "Copié !"
    }
    return "Copier le texte" 
  }

 /**
  * copie le texte de la div "tocopy" et change les valeurs associées au bouton
  */ 
  copier(){
    //copie du texte dans la div 
    const reponse = this.toCopy.nativeElement.innerHTML;
    function listener(e: any){
      e.clipboardData.setData("text/html", reponse);
      e.clipboardData.setData("text/plain", reponse);
      e.preventDefault();
    }

    document.addEventListener("copy", listener);
    document.execCommand("copy");
    document.removeEventListener("copy", listener);

    //changement du stype du bouton
    this.btnStyleCopie = 'btn-change';
    this.copie = true;
  }

  //change l'écriture dans le bouton
  sauvegarder(){
    if (this.save){
      return "Sauvegardé !"
    }
    return "Sauvegarder" 
  }

  //sauvegarde le mail dans la machine avec la fonction sendMail
  async saveMail(){
    let mail = new MailInfo();
    mail.subject = "prochain match SOCCER";
    mail.body = this.toCopy.nativeElement.innerHTML;
    mail.fileName = "soccer";
    await this.mailService.sendMail(mail);
    
    //changement du stype du bouton
    this.btnStyleSave = 'btn-change';
    this.save = true;
  }
}
