import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })};

  constructor(private http: HttpClient) {   
  }  
  
  protected url(param: string = ""): string {
    return `${environment.api_url}/investor/${param}`;
  }


  login(email: string, password: string): Observable<any> {
    return this.http.post(this.url('login'), {
      email,
      password
    }, this.httpOptions);
  }

  create(cpf: string, email: string, password: string): Observable<any> {
    return this.http.post(this.url('register'), {
      cpf,
      email,
      password
    }, this.httpOptions);
  }
}
