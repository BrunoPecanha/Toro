import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Asset } from '../model/asset';
import { CommandResult } from '../model/commands/commandResult';


@Injectable({
  providedIn: 'root'
})
export class EventService {

  registros: Asset[] = [];
  
  constructor(private http: HttpClient) {     
  }

  protected url(parametros: string): string {
    return `${environment.api_url}/${parametros}`;
  }

  async getTrends()  {
    try {
       this.http.get<CommandResult>(this.url("investor/trends"))
             .subscribe(resultado => console.log(resultado.log));
    }
    catch (error) {
      console.error(error);
      throw error;
  }  
}


  // async getTrends(): Promise<Array<any>>{
  //   debugger
  //   return await this.http.get<Array<any>>(this.url("investor/trends"))
  //   .toPromise().then(x =>
  //     console.log(x)      
  //   )

    
  

  // pipe(
  //   map(trends =>                 
  //     trends.map(
  //     trend => new Asset(trend.id, trend.currentPrince)
  //   ))
  // ).toPromise().then

  // async getHistory(id: number): Promise<{ data: Array<History>}> {
  //   try {
  //     const url = this.url(`?history=${id}`);      
  //     return await this.http.get<{ data: Array<History>, total: number }>(url)
  //     .pipe(map(x => {
  //       return {
  //         data: x.data
  //       };
  //     })).toPromise();
  //   } catch (error) {
  //     console.error(error);
  //     throw error;
  //   }
  // }

  // async create(name: string): Promise<any> {
  //   try {
  //     await this.http.post<Scene>(this.url(""), {
  //       name: name
  //     }).toPromise();
  //   } catch (error) {
  //       const httpError: HttpErrorResponse = error;
  //       if (Array.isArray(httpError.error)) {
  //           throw error
  //       } else {
  //         throw error
  //       }
  //   }
  // }

  // async update(
  //   id: number,
  //   nextState: number, 
  //   operationHour : Date
  // ): Promise<any> {
  //   try {
  //     await this.http.put<Scene>(this.url(""), {
  //       id: id,
  //       nextState: nextState,
  //       operationHour: operationHour       
  //     }).toPromise();
  //   } catch (error) {
  //     const httpError: HttpErrorResponse = error;
  //     if (Array.isArray(httpError.error)) {
  //       throw error;
  //     } else {
  //       throw error;
  //     }
  //   }
  // }  
}

