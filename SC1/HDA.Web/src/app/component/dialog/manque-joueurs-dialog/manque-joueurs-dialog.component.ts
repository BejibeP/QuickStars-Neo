import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { JoueurFromMatchInfo } from 'src/app/model/joueur-from-match-info';
import { MailInfo } from 'src/app/model/mail-info';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { JoueurMatchService } from 'src/app/service/joueur-match.service';
import { MailSoccerService } from 'src/app/service/mail-soccer.service';

@Component({
  selector: 'app-manque-joueurs-dialog',
  templateUrl: './manque-joueurs-dialog.component.html',
  styleUrls: ['./manque-joueurs-dialog.component.scss']
})
export class ManqueJoueursDialogComponent implements OnInit{

  @ViewChild('toCopy') toCopy!: ElementRef;
  btnStyleCopie = 'btn-default';
  btnStyleSave = 'btn-default';
  copie: boolean = false;
  save: boolean = false;
  currentRoute!: string;

  user!: string;
  joueursMatchVue!: JoueurFromMatchInfo[];
  nbTerrain!: number;
  nbJoueursManquant!: number;
  joueursTerrain1!: JoueurFromMatchInfo[];
  joueursTerrain2!: JoueurFromMatchInfo[];

  constructor(
    private mailService: MailSoccerService,
    private authenticationService: AuthenticationService,
    private joueurMatchService: JoueurMatchService
  ) { }

  async ngOnInit(): Promise<void> {
    this.authenticationService.getConnectedUser().subscribe(u => this.user = u);
    this.currentRoute = window.location.href;
    this.joueursMatchVue = await this.joueurMatchService.getJoueursFromMatch();
    this.nbTerrain = Math.floor(this.joueursMatchVue.length/10)+1;
    this.nbJoueursManquant = 10 - this.joueursMatchVue.length%10;
    this.joueursTerrain1 = this.joueursMatchVue.slice(0, 10);
    this.joueursTerrain2 = this.joueursMatchVue.slice(10, 20);
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
    await this.mailService.sendMail(mail);
    
    //changement du stype du bouton
    this.btnStyleSave = 'btn-change';
    this.save = true;
  }
}
