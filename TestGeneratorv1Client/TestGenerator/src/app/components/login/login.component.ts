import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { HttpErrorResponse } from '@angular/common/http';
import { AuthenticationService } from '../../services/authenticationService/authentication.service';
import { AuthService } from '../../services/authService/auth.service';
import { ApiService } from '../../services/apiService/api.service';
import { catchError } from 'rxjs/operators';
import { ObservableInput } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  email = '';
  password = '';
  errors: any;

  constructor(private authenticationService: AuthenticationService,
              private authService: AuthService,
              private apiService: ApiService,
              private router: Router) {}

  login(): void {

    this.errors = undefined;
    this.authenticationService.login({
      email: this.email,
      password: this.password
    })
     .subscribe((response) => {
      localStorage.setItem('subjects' , JSON.stringify(response.userSubjects.subjects));
        localStorage.setItem('name' , JSON.stringify(response.userSubjects.name));
        localStorage.setItem('user' , JSON.stringify(response.userSubjects));
        localStorage.setItem('roleAppAdmin' , JSON.stringify(response.roleAppAdmin));
        localStorage.setItem('roleSystemAdmin' , JSON.stringify(response.roleSystemAdmin));


        this.authService.setToken(response.token);
        this.apiService.setToken();
        this.router.navigate(['/StartPage']);
      }, (err: HttpErrorResponse) => {
        this.errors = err.error.message;
      });

  }
}
