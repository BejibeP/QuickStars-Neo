import { TestBed } from '@angular/core/testing';

import { ImportMailsJoueursService } from './import-mails-joueurs.service';

describe('ImportMailsJoueursService', () => {
  let service: ImportMailsJoueursService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ImportMailsJoueursService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
