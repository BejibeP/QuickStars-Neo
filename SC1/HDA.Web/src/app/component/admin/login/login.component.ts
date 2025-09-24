import { Component } from '@angular/core';
import { Login } from '../../../model/admin/login';
import { AuthenticationService } from '../../../service/authentication.service';
import { Route, Router } from '@angular/router';
import { FormControl, FormGroup, NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  user: Login = new Login();
  hide = true;

  constructor(
    private authSrv: AuthenticationService,
    private router: Router,
    ){}

  async connexion(form: NgForm){
    await this.authSrv.login(this.user);
    form.resetForm();
    this.router.navigateByUrl('/create')
  }

  
}
