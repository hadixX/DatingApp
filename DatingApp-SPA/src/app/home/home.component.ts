import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  regmode = false;
  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  regToggle() {
    this.regmode = true;
  }

  cancelMode(regmode: boolean){
    this.regmode = regmode;
  }

}
