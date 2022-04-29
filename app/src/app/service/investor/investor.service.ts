import { Injectable, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Asset } from 'src/app/model/asset';
import {  map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class InvestorService implements OnInit {
  
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getTrendsAsync();    
  }

  protected url(param: string = ""): string {
    return `${environment.api_url}/investor/${param}`;
  }

  async getTrendsAsync(): Promise<{ data: Array<Asset>, valid: boolean, message: string }> {
      try {
        return await this.http.get<{ data: Array<Asset>, valid: boolean, message: string }>(this.url())
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
  
  async getUserPositionAsync(id: number) : Promise<{ data: any, valid: boolean, message: string }> {
    try {
      return await this.http.get<{ data: Array<Asset>, valid: boolean, message: string }>(this.url(`userPosition?id=${id}`))
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