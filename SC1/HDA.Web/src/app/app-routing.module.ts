import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateMatchComponent } from './component/create-match/create-match.component';
import { AccueilComponent } from './component/accueil/accueil.component';
import { JoueurMatchComponent } from './component/joueur-match/joueur-match.component';
import { JoueursComponent } from './component/joueurs/joueurs.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { LoginComponent } from './component/admin/login/login.component';
import { RepasComponent } from './component/repas/repas.component';
import { authGuard } from './guard/auth.guard';
import { MembersComponent } from './component/HDA/members/members.component';
import { BookingsComponent } from './component/HDA/bookings/bookings.component';
import { CalendarComponent } from './component/HDA/calendar/calendar.component';

const routes: Routes = [
  
  { path: '', redirectTo: 'home', pathMatch: 'full'},
  { path: 'create', component: CreateMatchComponent, canActivate: [authGuard] },
  { path: 'home', component: AccueilComponent },
  { path: 'match', component: JoueurMatchComponent },
  { path: 'joueurs', component: JoueursComponent, canActivate: [authGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'repas', component: RepasComponent},
  { path: 'members', component: MembersComponent , canActivate: [authGuard]},
  { path: 'bookings', component: BookingsComponent, canActivate: [authGuard]},
  { path: 'calendar', component: CalendarComponent },
  { path: '**', redirectTo: 'home'}, //wildcard route
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes),
    MatFormFieldModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
