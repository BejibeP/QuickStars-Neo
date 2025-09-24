import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MailDest } from 'src/app/model/mail-dest';
import { MailSoccerService } from 'src/app/service/mail-soccer.service';

@Component({
  selector: 'app-copie-mail-joueurs-match-dialog',
  templateUrl: './copie-mail-joueurs-match-dialog.component.html',
  styleUrls: ['./copie-mail-joueurs-match-dialog.component.scss']
})
export class CopieMailJoueursMatchDialogComponent {


  copie!: MailDest;

  openSnackBar(message: string, action: string){
    this._snackBar.open(message, action, { duration: 5000 })
  };

  constructor(
    private _mailService: MailSoccerService,
    private _snackBar: MatSnackBar
  ) { }
  
  async ngOnInit(): Promise<void> {
    this.copie = await this._mailService.getListMailsMatchJoueurs();
 }

  copieMails(type: string){

    let response: string | undefined;
    if (type == "sopra"){
      response = this.copie.to;
    }
    if (type == "ext"){
      response = this.copie.cc;
    }
    if (type == "all"){
      response = `${this.copie.to} ${this.copie.cc}`;
    }
    
    function listener(e: any){
      e.clipboardData.setData("text/html", response);
      e.clipboardData.setData("text/plain", response);
      e.preventDefault();
    }

    document.addEventListener("copy", listener);
    document.execCommand("copy");
    document.removeEventListener("copy", listener);
    this.openSnackBar(`La liste été copiée dans le presse papier.`, "OK");
    
  }

}

