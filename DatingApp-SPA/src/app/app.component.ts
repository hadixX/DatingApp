import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import {JwtHelperService} from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'DatingApp-SPA';
  jwthelper = new JwtHelperService();
  constructor(private authSerivce: AuthService){}

  ngOnInit(){
    const token = localStorage.getItem('token');
    if (token){
      this.authSerivce.decoded = this.jwthelper.decodeToken(token);
    }
  }
}
