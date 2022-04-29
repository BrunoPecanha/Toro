import {  HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {LOCALE_ID, DEFAULT_CURRENCY_CODE} from '@angular/core';
import localePt from '@angular/common/locales/pt';

registerLocaleData(localePt, 'pt');

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './components/home/home.component';
import { FormsModule } from '@angular/forms';
import { UserComponent } from './components/user/user.component';
import { registerLocaleData } from '@angular/common';


@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    HomeComponent
    
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule 
  ],
  providers: [ {
    provide: LOCALE_ID,
    useValue: 'pt-BR'
},
{
    provide:  DEFAULT_CURRENCY_CODE,
    useValue: 'BRL'
},],
  bootstrap: [AppComponent]
})
export class AppModule { }
