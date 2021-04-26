import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private ApiUrl=environment.ApiUrl
  constructor(private httpclient:HttpClient) { }

  ApiCall(){
    return this.httpclient.get(this.ApiUrl + "api/getName")
  }

}
