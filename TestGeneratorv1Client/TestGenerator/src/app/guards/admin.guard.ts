import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/authService/auth.service';


@Injectable()
export class AdminGuard {

    constructor(private router: Router, private authService: AuthService) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.authService.isAdmin() || this.authService.isSystemAdmin()) {
            return true;
        } else {
            this.router.navigate(['/login']);
            return false;
        }
    }
}
