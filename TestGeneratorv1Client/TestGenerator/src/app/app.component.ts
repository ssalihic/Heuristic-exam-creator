import{Component}from'@angular/core';
import { StatusService } from './services/statusService/status.service';
import { AuthService } from './services/authService/auth.service';
import { AuthenticationService } from './services/authenticationService/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  /**
   *
   */
  title = 'app';
  constructor(public authService: AuthService,
              private authenticationService: AuthenticationService) {}

  ngOnInit(): void {
    this.authService.authStatus.next(this.authService.isAuthenticated());
  }

  logOut(): void {
    const userId = JSON.parse(localStorage.getItem('user')).id;
    this.authenticationService.logout(userId);
  }

}
