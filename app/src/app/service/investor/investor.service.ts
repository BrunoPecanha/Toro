import { Injectable, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { CommandResult } from 'src/app/model/commands/commandResult';
import { Asset } from 'src/app/model/asset';





@Injectable({
  providedIn: 'root'
})
export class InvestorService implements OnInit {

  registros: Asset[] = [];
  
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getTrendsAsync();
    
  }

  protected url(param: string = ""): string {
    return `${environment.api_url}/event/${param}`;
  }

  async getTrendsAsync()  {
      try {
        this.http.get<CommandResult>(this.url())
        .toPromise().then(x =>console.log(x))
      }
      catch (error) {
        console.error(error);
        throw error;
    }  
  }

  async getUserPositionAsync(id: number)  {    
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

