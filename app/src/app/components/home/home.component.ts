import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'src/app/helpers/dialog-service';
import { Asset } from 'src/app/model/asset';
import { InvestorService } from 'src/app/service/investor/investor.service';
import { TokenStorageService } from '../../service/auth/token-storage.service';
import { Patrimony } from 'src/app/model/patrimony';
import { EventService } from 'src/app/service/event/event.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  topAssets: Asset[] = [];
  totalInvested: number = 0;
  patrimony: Patrimony = { accountAmount: 0, totalAmount: 0, assets: null};
  isLoggedIn = false;

  constructor(private eventService: EventService, private dialog: DialogService, private investorService: InvestorService, private tokenStorage: TokenStorageService, private router: Router){    
  }

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.router.navigate(['home'])
      this.getTrendsAsync();
      this.getUserPositionAsync(5);
    }
    else
      this.router.navigate(['login'])    
  }

  async getTrendsAsync(): Promise<void> {
    try {
      const result = await this.investorService.getTrendsAsync();
      this.topAssets = result.data;
    } catch (error) {
      this.dialog.showErr('Erro', 'Ocorreu um erro ao carregar as informações de ações mais negociadas');      
    }
  }

  async getUserPositionAsync(id: number): Promise<void> {
    try {
      const result = await this.investorService.getUserPositionAsync(id);
      this.patrimony = result.data;     
      
      if (!result.data)
        this.dialog.info(result.message);
      
    } catch (error) {
      this.dialog.showErr('Erro', 'Ocorre um erro ao tentar carregar as informações da carteira do usuário.');      
    }
  }

  async assetPurshaseOrderAsync(): Promise<void> {
    try {
      const result = await this.eventService
      .orderAsync(
            {
              investorId: 0,
              assetId: "VALE2",
              originBank: 0,
              originBranch: 0,
              cpf: '13676616766',
              amount: 0,
              eventType: 0
            }
      );
      this.patrimony = result.data;     
      
      if (!result.data)
        this.dialog.info(result.message);

    } catch (error) {
      this.dialog.showErr('Erro', 'Ocorre um erro ao tentar carregar as informações da carteira do usuário.');      
    }
  }

  async depositOrderAsync(): Promise<void> {
    try {
      const result = await this.eventService
      .orderAsync(
            {
              // investorId: number;
              // assetId: string;
              // originBank: number;
              // originBranch: number
              // cpf: string;
              // amount: number
              // eventType: number; 
            }
      );
      this.patrimony = result.data;     
      
      if (!result.data)
        this.dialog.info(result.message)
      else {

      }
    } catch (error) {
      this.dialog.showErr('Erro', 'Ocorre um erro ao tentar carregar as informações da carteira do usuário.');      
    }
  }

  logout(): void {    
    this.tokenStorage.signOut();
    window.location.reload();
  }
}


