import { TestBed } from '@angular/core/testing';

import { JoueurMatchService } from './joueur-match.service';

describe('JoueurMatchService', () => {
  let service: JoueurMatchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JoueurMatchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
