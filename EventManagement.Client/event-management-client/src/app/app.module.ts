import {NgModule, APP_INITIALIZER} from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS, HttpClient} from '@angular/common/http';
import {BrowserModule} from '@angular/platform-browser';

import {CoreModule} from './core/core.module';
import {SharedModule} from './shared/shared.module';
import {ThemeModule} from './theme/theme.module';
import {RoutesModule} from './routes/routes.module';
import {AppComponent} from './app.component';

import {DefaultInterceptor} from '@core';
import {StartupService} from '@core';

export function StartupServiceFactory(startupService: StartupService) {
  return () => startupService.load();
}

import {FormlyModule} from '@ngx-formly/core';
import {ToastrModule} from 'ngx-toastr';
import {TranslateModule, TranslateLoader} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MsalModule, MsalInterceptor} from '@azure/msal-angular';
import {environment} from '@env/environment';
import {MsalUserServiceService} from '@shared/services/msal-user-service.service';

// Required for AOT compilation
export function TranslateHttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

export const protectedResourceMap: any =
  [
    [environment.SERVER_URL, environment.scopeUri
    ]
  ];

@NgModule({
  declarations: [AppComponent],
  imports: [
    MsalModule.forRoot({
      clientID: environment.uiClienId,
      authority: `https://login.microsoftonline.com/${environment.tenantId}`,
      protectedResourceMap,
      redirectUri: environment.redirectUrl
    }),
    BrowserModule,
    HttpClientModule,
    CoreModule,
    SharedModule,
    ThemeModule,
    RoutesModule,
    FormlyModule.forRoot(),
    ToastrModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: TranslateHttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
    BrowserAnimationsModule,
  ],
  providers: [
    MsalUserServiceService,
    { provide: HTTP_INTERCEPTORS, useClass: MsalInterceptor, multi: true },
    StartupService,
    {
      provide: APP_INITIALIZER,
      useFactory: StartupServiceFactory,
      deps: [StartupService],
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
}
