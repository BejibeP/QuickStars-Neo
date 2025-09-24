import { TestBed } from '@angular/core/testing';

import { MailSoccerService } from './mail-soccer.service';

describe('MailSoccerServiceyService', () => {
  let service: MailSoccerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MailSoccerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
