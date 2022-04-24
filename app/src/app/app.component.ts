import { Component } from '@angular/core';
import { InvestorService } from './service/investor.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ToroApp';  

  constructor(eventService: InvestorService){    
    eventService.getTrendsAsync();
    eventService.getUserPositionAsync(5);
    
  }
}
