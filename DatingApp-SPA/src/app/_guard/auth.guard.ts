import { Injectable } from '@angular/core';
import { CanActivate, Router} from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alert: AlertifyService
  ) {}

  canActivate(): boolean {
    if (this.authService.loggedin()) {
      return true;
    }

    this.alert.error('You Shall Not Pass !!!!');
    this.router.navigate(['/home']);
    return false;
  }


}
