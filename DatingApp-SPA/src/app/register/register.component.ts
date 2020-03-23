import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from '@angular/compiler/src/util';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() CancelReg = new EventEmitter();
  model: any = {};
  constructor(private auth: AuthService) { }

  ngOnInit() {
  }

  Register(){
    this.auth.register(this.model).subscribe(() => {
      console.log('Registeration Successful');
    // tslint:disable-next-line: no-shadowed-variable
    }, error =>{
      console.log(error);
    });
  }

  Cancel(){
    this.CancelReg.emit(false);
    console.log('canceled successfuly');
  }

}
