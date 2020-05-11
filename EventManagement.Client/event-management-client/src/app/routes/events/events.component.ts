import {Component, OnInit} from '@angular/core';
import {EventsDataService} from '@shared/services/events-data.service';
import {Event} from '@core/models/Event';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {

  events: Event[];

  constructor(private eventsDataService: EventsDataService) {
  }

  ngOnInit(): void {
    this.eventsDataService.getAllEvents().subscribe(
      (values: Event[]) => {
        this.events = values;
      }
    );
  }

  getUser(){
    this.eventsDataService.getCurrentUserInfo();
  }

  logout(){
    this.eventsDataService.logout();
  }

}
