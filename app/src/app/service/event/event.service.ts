import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommandResult } from 'src/app/model/commands/commandResult';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http: HttpClient) {   
  }
  
  protected url(param: string = ""): string {
    return `${environment.api_url}/register/${param}`;
  }

  async orderAsync(eventDto: any): Promise<any> {
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
