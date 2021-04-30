import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CheckLoginService } from '../_services/check-login.service';
import { EmployeesService } from '../_services/employees.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {


  loggedIn:boolean;
  model:any={}  
  constructor(private _account:EmployeesService,private _checklogin:CheckLoginService,private route:Router) { }



  ngOnInit(): void {
    this.loggedIn=this._checklogin.loggedIn;
    this.model=this._checklogin.model;
    console.log(this.loggedIn);
    console.log(this.model.token);    
    if(this.loggedIn){
      this.Submit(this.model.token);
    }
    else{
      this.route.navigate(['../Login']);
    }
  }

  Submit(str:any){    
      this._account.Get(str).subscribe(success=>{        
        console.log(success);
      },(error=>{
        alert();
        console.log(error);
      }))
    }
}
