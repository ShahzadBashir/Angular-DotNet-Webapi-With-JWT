import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/-account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';
  ngOnInit(): void {
 
    }  
}
