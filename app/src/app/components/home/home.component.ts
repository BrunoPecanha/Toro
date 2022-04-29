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
  user: any;
  asset: any;

  constructor(private eventService: EventService, private dialog: DialogService, 
    private investorService: InvestorService, private tokenStorage: TokenStorageService, 
    private router: Router){}

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.router.navigate(['home'])
      this.getTrendsAsync();
      this.user = this.tokenStorage.getUser();
      this.getUserPositionAsync(this.user.id);
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
        this.dialog.info('Atenção', result.message);
      
    } catch (error) {
      this.dialog.showErr('Erro', 'Ocorre um erro ao tentar carregar as informações da carteira do usuário.');      
    }
  }

  async assetPurshaseOrderAsync(): Promise<void> {
    try {

      let qtd = (document.getElementById('qtd') as HTMLInputElement).value;
      if (!qtd || qtd == '0') {
        this.dialog.info('Atenção', "Quantidade não pode ser menor que 1")
        return;
      }

      const result = await this.eventService
      .orderAsync(
            {
              userId: this.user.id,
              assetId: this.asset,            
              amount: qtd,
              eventType: 1
            }
      );

       if (result.valid){
        await this.dialog.showAlert('Sucesso', result.message);
        this.dialog.reloadPage();
       }       

    } catch (error: any) {
      this.dialog.info('Atenção', error.error.message);      
    }
  }

  async depositOrderAsync(): Promise<void> {    
    try {
      let bankCode = (document.getElementById('bankCode') as HTMLInputElement).value;
      let agency = (document.getElementById('agency') as HTMLInputElement).value;
      let cpf = (document.getElementById('cpf') as HTMLInputElement).value;
      let authCode = (document.getElementById('authCode') as HTMLInputElement).value;
      let amount = (document.getElementById('amount') as HTMLInputElement).value;


      if (this.isValid(bankCode, agency, cpf, authCode, amount))
      {
        this.dialog.info('Atenção', "Todas as informações devem ser preenchidas corretamente para o depósito")
        return;
      }
      
      const result = await this.eventService
      .orderAsync(
            {
              userId: this.user.id,
              amount: amount,  
              cpf: cpf,       
              originBank: bankCode,
              originBranch: agency,              
              eventType: 0
            }
      );            
      if (result.valid){
        await this.dialog.showAlert('Sucesso', result.message);
        this.dialog.reloadPage();
       }    
    } catch (error: any) {
      this.dialog.info('Atenção', error.error.message);      
    }
  }

  async logout() {    
    if (await this.dialog.confirm('Sair', 'Deseja realmente sair?')){
      this.tokenStorage.signOut();
      window.location.reload();
    }
  }

  public getAssetSelected(asset: string) {
    this.asset = asset;
  }

  private isValid(bankCode: string,agency: string,  cpf: string, authCode: string, amount: string){
    return ((!bankCode || bankCode == '0') || 
          (!agency || agency == '0') ||
          (!cpf || cpf == '0') ||
          (!authCode || authCode == '0') ||
          (!amount || amount == '0'));
  }
}


