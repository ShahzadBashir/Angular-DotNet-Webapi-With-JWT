import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {
  private ApiUrl=environment.ApiUrl
  constructor(private httpclient:HttpClient) { }


  

  Get(str:any){
    let httpHeader=new HttpHeaders({
      'Authorization':'Bearer '+ str
    });
    return this.httpclient.get(this.ApiUrl + "/getName",{headers:httpHeader});
  }
}
