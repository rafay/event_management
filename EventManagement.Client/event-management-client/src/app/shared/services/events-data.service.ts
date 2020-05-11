import {Injectable} from '@angular/core';
import {environment} from '@env/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {MsalUserServiceService} from '@shared/services/msal-user-service.service';
import {Observable} from 'rxjs';
import {Event} from '@core/models/Event';

@Injectable({
  providedIn: 'root'
})
export class EventsDataService {
  private url = `${environment.SERVER_URL}api/events`;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient, private msalService: MsalUserServiceService) {
  }

  getAllEvents(): Observable<Event[]> {
    this.httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: 'Bearer ' + this.msalService.GetAccessToken()
      })
    };

    return this.http.get(this.url, this.httpOptions)
      .pipe((response: any) => {
        return response;
      });
  }

  getCurrentUserInfo() {
    this.msalService.getCurrentUserInfo();
  }

  logout() {
    this.msalService.logout();
  }
}
