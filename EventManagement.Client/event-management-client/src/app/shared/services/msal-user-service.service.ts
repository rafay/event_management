import {Injectable} from '@angular/core';
import * as Msal from 'msal';
import {environment} from '@env/environment';
import {Observable} from 'rxjs';

@Injectable()
export class MsalUserServiceService {
  private accessToken: any;
  public clientApplication: Msal.UserAgentApplication = null;

  constructor() {
    this.clientApplication = new Msal.UserAgentApplication(
      environment.uiClienId,
      `https://login.microsoftonline.com/${environment.tenantId}`,
      this.authCallback,
      {
        storeAuthStateInCookie: true
      }
    );
  }

  public GetAccessToken(): Observable<any> {
    if (sessionStorage.getItem('msal.idtoken') !== undefined && sessionStorage.getItem('msal.idtoken') != null) {
      this.accessToken = sessionStorage.getItem('msal.idtoken');
    }
    return this.accessToken;
  }

  public authCallback(errorDesc, token, error, tokenType) {
    if (token) {
    } else {
      console.log(error + ':' + errorDesc);
    }
  }

  public getCurrentUserInfo() {
    const user = this.clientApplication.getUser();
    // alert(user.name);
  }

  public logout() {
    this.clientApplication.logout();
  }
}
