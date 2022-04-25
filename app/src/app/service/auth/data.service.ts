import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  getLoginArea(): Observable<any> {
    return this.http.get(`${environment.api_url}` + '', { responseType: 'text' });
  }

  getHomeContent(): Observable<any> {
    return this.http.get(`${environment.api_url}` + '/home', { responseType: 'text' });
  } 
}