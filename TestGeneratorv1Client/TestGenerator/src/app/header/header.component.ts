import { Component, OnInit, Output } from '@angular/core';
import { AuthService } from '../services/authService/auth.service';
import { Route, Router } from '@angular/router';
import { EventEmitter } from 'protractor';
import { AuthenticationService } from '../services/authenticationService/authentication.service';
import { registerLocaleData } from '@angular/common';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent  implements OnInit {
  admin = false;
  systemAdmin = false;
  language = 'en';
  name = '';
  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.admin = this.isAdmin();
    this.systemAdmin = this.isSystemAdmin();
    this.name = JSON.parse(localStorage.getItem('user')).name;
  }
  logOut() {
    const userId = JSON.parse(localStorage.getItem('user')).id;

    this.authenticationService.logout(userId);
  }
  isSystemAdmin() {
    return JSON.parse(localStorage.getItem('roleSystemAdmin'));
  }
  isAdmin() {
    return JSON.parse(localStorage.getItem('roleAppAdmin'));
  }

}
