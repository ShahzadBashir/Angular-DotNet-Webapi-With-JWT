import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../_services/-account.service';
import { CheckLoginService } from '../_services/check-login.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  loggedIn:boolean;
  model:any={};
  RegisterForm:FormGroup;
  constructor(private _account:AccountService,private _checkLogin:CheckLoginService) { }

  ngOnInit(): void {
    this.InitForm();
  }

  InitForm(){
    this.RegisterForm=new FormGroup({
      UserName:new FormControl(null,[Validators.required]),
      Password:new FormControl(null,[Validators.required])
    });
  }
  Submit(){
    if(this.RegisterForm.valid){
      this._account.Login(this.RegisterForm.value).subscribe(success=>{
        this.RegisterForm.reset();
        this.loggedIn=true;
        this.model=success;

        this._checkLogin.setLoginDetails(true,success);
        console.log(this.model);
        console.log(this.loggedIn);
      })
    }
    
  }
}
