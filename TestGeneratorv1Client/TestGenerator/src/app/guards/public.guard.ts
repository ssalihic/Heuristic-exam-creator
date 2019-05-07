import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/authService/auth.service';


/**
 * This guard prevents user from accessing pages like login when he is already
 * authenticated
 */
@Injectable()
export class PublicGuard {
    constructor(private router: Router,
                private auth: AuthService) {}

    canActivate(route: ActivatedRouteSnapshot): boolean {

        if (this.auth.isAuthenticated()) {

            this.router.navigate(['/AddQuestion']);
            return false;
        } else if (!this.auth.isAuthenticated()) {
            return true;
        }
        return !this.auth.isAuthenticated();
    }
}
