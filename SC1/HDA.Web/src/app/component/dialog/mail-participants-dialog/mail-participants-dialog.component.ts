import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { JoueurFromMatchInfo } from 'src/app/model/joueur-from-match-info';
import { MailInfo } from 'src/app/model/mail-info';
import { Match } from 'src/app/model/match/match';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { JoueurMatchService } from 'src/app/service/joueur-match.service';
import { MailSoccerService } from 'src/app/service/mail-soccer.service';
import { MatchService } from 'src/app/service/match.service';

@Component({
  selector: 'app-mail-participants-dialog',
  templateUrl: './mail-participants-dialog.component.html',
  styleUrls: ['./mail-participants-dialog.component.scss']
})
export class MailParticipantsDialogComponent implements OnInit {

  @ViewChild('toCopy') toCopy!: ElementRef;
 
  btnStyleCopie = 'btn-default';
  btnStyleSave = 'btn-default';
  matchOuvert!: Match;
  user!: string;
  joueursMatchVue!: JoueurFromMatchInfo[];
  joueursTerrain1!: JoueurFromMatchInfo[];
  joueursTerrain2!: JoueurFromMatchInfo[];
  deuxTerrain = false;
  copie: boolean = false;
  save: boolean = false;

  constructor(
    private matchService: MatchService,
    private joueurMatchService: JoueurMatchService,
    private authenticationService: AuthenticationService,
    private mailService: MailSoccerService,
    ){}

  async ngOnInit(): Promise<void> {
    this.matchOuvert = await this.matchService.getOpenMatch();
    this.authenticationService.getConnectedUser().subscribe(u => this.user = u);
    this.joueursMatchVue = await this.joueurMatchService.getJoueursFromMatch();
    this.joueursTerrain1 = this.joueursMatchVue.slice(0, 10);
    this.joueursTerrain2 = this.joueursMatchVue.slice(10, 20)
    //s'il y a 20 joueurs ou plus, alors il y aura 2 terrains
    if(this.joueursMatchVue.length >= 20){
      this.deuxTerrain = true;
    }
    
  }
  
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
    await this.mailService.sendMailJoueursMatch(mail);
    
    //changement du stype du bouton
    this.btnStyleSave = 'btn-change';
    this.save = true;
  }
}
