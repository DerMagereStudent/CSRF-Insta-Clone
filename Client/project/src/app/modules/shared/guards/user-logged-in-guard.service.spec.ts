/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UserLoggedInGuard } from './user-logged-in-guard.service';

describe('Service: UserLoggedInGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserLoggedInGuard]
    });
  });

  it('should ...', inject([UserLoggedInGuard], (service: UserLoggedInGuard) => {
    expect(service).toBeTruthy();
  }));
});
