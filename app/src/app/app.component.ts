import { Component } from '@angular/core';
import { CommandResult } from './model/commands/commandResult';
import { EventService } from './service/event.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ToroApp';

  public trends: CommandResult = new CommandResult;

  constructor(eventService: EventService){    
    eventService.getTrends();
    eventService.getUserPosition(5);
    
  }
}
