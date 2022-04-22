import { Injectable, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Asset } from '../model/asset';
import { CommandResult } from '../model/commands/commandResult';


@Injectable({
  providedIn: 'root'
})
export class EventService implements OnInit {

  registros: Asset[] = [];
  
  constructor(private http: HttpClient) {     
    
  }

  ngOnInit(): void {
    this.getTrends();
    
  }

  protected url(param: string = ""): string {
    return `${environment.api_url}/investor/${param}`;
  }

  async getTrends()  {
      try {
        this.http.get<CommandResult>(this.url())
        .toPromise().then(x =>console.log(x))
      }
      catch (error) {
        console.error(error);
        throw error;
    }  
  }

  async getUserPosition(id: number)  {
    debugger
      try {
        this.http.get<CommandResult>(this.url(`userPosition?id=${id}`))
        .toPromise().then(x =>console.log(x))
      }
      catch (error) {
        console.error(error);
        throw error;
    }  
  }
}

