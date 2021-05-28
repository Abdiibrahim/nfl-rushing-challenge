import { TestBed } from '@angular/core/testing';

import { RushingService } from './rushing.service';

describe('RushingService', () => {
  let service: RushingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RushingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
