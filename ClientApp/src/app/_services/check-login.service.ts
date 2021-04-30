import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CheckLoginService {

  loggedIn:boolean;
  model:any={};

  constructor() { }

  setLoginDetails(log:boolean,md:any){
    this.loggedIn=log;
    this.model=md;
  }

}
