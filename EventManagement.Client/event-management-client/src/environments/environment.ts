// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  SERVER_URL: 'https://localhost:44330/',
  production: false,
  useHash: true,
  hmr: false,
  scopeUri: ['api://988bbee6-d68f-4260-b139-9fbf502effe5/Events.Read.Write'],
  tenantId: '7ec2fb67-c55a-4e8f-9d47-2336089fdc57',
  uiClienId: '9f255814-3910-4ce0-b041-774fc448681d',
  redirectUrl: 'http://localhost:4200'
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
