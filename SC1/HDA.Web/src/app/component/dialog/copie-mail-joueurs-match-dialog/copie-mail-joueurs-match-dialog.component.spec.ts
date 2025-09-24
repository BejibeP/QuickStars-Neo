import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CopieMailJoueursMatchDialogComponent } from './copie-mail-joueurs-match-dialog.component';

describe('CopieMailJoueursMatchDialogComponent', () => {
  let component: CopieMailJoueursMatchDialogComponent;
  let fixture: ComponentFixture<CopieMailJoueursMatchDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CopieMailJoueursMatchDialogComponent]
    });
    fixture = TestBed.createComponent(CopieMailJoueursMatchDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
