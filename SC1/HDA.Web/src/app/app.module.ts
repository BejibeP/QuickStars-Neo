import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { CreateMatchComponent } from './component/create-match/create-match.component';
import { JoueursComponent } from './component/joueurs/joueurs.component';
import { MenuComponent } from './component/menu/menu.component';
import { AccueilComponent } from './component/accueil/accueil.component';
import { JoueurMatchComponent } from './component/joueur-match/joueur-match.component';
import { LoginComponent } from './component/admin/login/login.component';

import { CreateMatchDialogComponent } from './component/dialog/create-match-dialog/create-match-dialog.component';
import { DeleteMatchDialogComponent } from './component/dialog/delete-match-dialog/delete-match-dialog.component';
import { DeleteRepasDialogComponent } from './component/dialog/delete-repas-dialog/delete-repas-dialog.component';
import { MailInvitationDialogComponent } from './component/dialog/mail-invitation-dialog/mail-invitation-dialog.component';
import { MailParticipantsDialogComponent } from './component/dialog/mail-participants-dialog/mail-participants-dialog.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatDialogModule } from '@angular/material/dialog';
import { RepasComponent } from './component/repas/repas.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { ModifyRepasDialogComponent } from './component/dialog/modify-repas-dialog/modify-repas-dialog.component';
import { CopieMailJoueursDialogComponent } from './component/dialog/copie-mail-joueurs-dialog/copie-mail-joueurs-dialog.component';
import { CopieMailJoueursMatchDialogComponent } from './component/dialog/copie-mail-joueurs-match-dialog/copie-mail-joueurs-match-dialog.component';
import { DeleteJoueurDialogComponent } from './component/dialog/delete-joueur-dialog/delete-joueur-dialog.component';
import { ManqueJoueursDialogComponent } from './component/dialog/manque-joueurs-dialog/manque-joueurs-dialog.component';
import { MembersComponent } from './component/HDA/members/members.component';
import { BookingsComponent } from './component/HDA/bookings/bookings.component';
import { CalendarComponent } from './component/HDA/calendar/calendar.component';


@NgModule({
  declarations: [
    AppComponent,
    CreateMatchComponent,
    MenuComponent,
    AccueilComponent,
    JoueurMatchComponent,
    JoueursComponent,
    LoginComponent,
    CreateMatchDialogComponent,
    RepasComponent,
    DeleteMatchDialogComponent,
    DeleteRepasDialogComponent,
    MailInvitationDialogComponent,
    MailParticipantsDialogComponent,
    ModifyRepasDialogComponent,
    CopieMailJoueursDialogComponent,
    CopieMailJoueursMatchDialogComponent,
    DeleteJoueurDialogComponent,
    ManqueJoueursDialogComponent,
    MembersComponent,
    BookingsComponent,
    CalendarComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    RouterModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatSlideToggleModule, MatSnackBarModule,
    MatFormFieldModule, MatSelectModule, MatInputModule, MatButtonModule, 
    MatIconModule, MatCheckboxModule, MatDividerModule, MatExpansionModule, MatToolbarModule, MatDialogModule,
    MatPaginatorModule, MatSortModule, MatTableModule, MatListModule,
    ClipboardModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
