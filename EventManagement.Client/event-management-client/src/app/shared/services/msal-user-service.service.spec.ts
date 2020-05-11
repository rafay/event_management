import { TestBed } from '@angular/core/testing';

import { MsalUserServiceService } from './msal-user-service.service';

describe('MsalUserServiceService', () => {
  let service: MsalUserServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MsalUserServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
