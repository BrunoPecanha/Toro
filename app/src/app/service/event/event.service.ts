import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {  map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private http: HttpClient) {   
  }
  
  protected url(param: string = ""): string {
    return `${environment.api_url}/event/${param}`;
  }

  async orderAsync(eventDto: any): Promise<{ data: any, valid: boolean, message: string }> {    
    try {
      return await this.http.post<{ data: any, valid: boolean, message: string }>(this.url(), eventDto)
      .pipe(map(x => {
        return {
          message: x.message,
          valid: x.valid,
          data: x.data
        }
        })).toPromise();         
    } catch (error) {
      throw error;
    }
  } 
}
