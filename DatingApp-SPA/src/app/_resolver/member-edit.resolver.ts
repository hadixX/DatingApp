import {Injectable} from "@angular/core";
import {User} from "../_models/user";
import {ActivatedRouteSnapshot, Resolve, Router} from "@angular/router";
import { UserService } from "../_services/user.service";
import { AlertifyService } from "../_services/alertify.service";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { AuthService } from '../_services/auth.service';
@Injectable()
export class MemberEditResolver implements Resolve<User>{
    constructor(private userService:UserService,private router:Router,private alertify:AlertifyService,private authS:AuthService){}

    resolve(route:ActivatedRouteSnapshot): Observable<User>{
        return this.userService.getUser(this.authS.decoded.nameid).pipe(
            catchError(error =>{
                this.alertify.error('Problem retreving your data');
                this.router.navigate(['/members']);
                return of(null);
            })
        )
    }
}