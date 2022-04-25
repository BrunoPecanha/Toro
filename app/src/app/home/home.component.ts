import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenStorageService } from '../service/auth/token-storage.service';
import { InvestorService } from '../service/investor/investor.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  isLoggedIn = false;

  constructor(eventService: InvestorService,  private _router: Router, private tokenStorage: TokenStorageService, private router: Router){    
    // eventService.getTrendsAsync();
    // eventService.getUserPositionAsync(5);
    
  }

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.router.navigate(['home'])
    }
    else
      this.router.navigate(['login'])    
  }

  logout(): void {    
    this.tokenStorage.signOut();
    window.location.reload();
  }
}


