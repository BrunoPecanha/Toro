import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CommandResult } from '../model/commands/commandResult';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http: HttpClient) {   
  }
  
  protected url(param: string = ""): string {
    return `${environment.api_url}/event/${param}`;
  }

  async orderAsync(eventDto = {
    investorId: 0,
    assetId: "VALE2",
    originBank: 0,
    originBranch: 0,
    cpf: '13676616766',
    amount: 0,
    eventType: 0    
    }): Promise<any> {
    try {
      await this.http.post<CommandResult>(this.url(), eventDto)
      .toPromise();
    } catch (error) {
        if (Array.isArray(error)) {
            throw error
        } else {
          throw error
      }
    }
  }
}
