import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'src/app/helpers/dialog-service';
import { Asset } from 'src/app/model/asset';
import { InvestorService } from 'src/app/service/investor/investor.service';
import { TokenStorageService } from '../../service/auth/token-storage.service';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  topAssets: Asset[] = [];
  isLoggedIn = false;
  valorAcao: number = 0;
  color = 'btn-success'
  constructor(private dialog: DialogService, private eventService: InvestorService, private tokenStorage: TokenStorageService, private router: Router){    
  }

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.router.navigate(['home'])
      this.getTrends();
    }
    else
      this.router.navigate(['login'])    
  }

  async getTrends(): Promise<void> {
    try {
      const result = await this.eventService.getTrendsAsync();
      this.topAssets = result.data;
    } catch (error) {
      this.dialog.showErr('Erro', 'Ocorreu um erro ao carregar as informações de ações mais negociadas');      
    }
  }

  logout(): void {    
    this.tokenStorage.signOut();
    window.location.reload();
  }
}


