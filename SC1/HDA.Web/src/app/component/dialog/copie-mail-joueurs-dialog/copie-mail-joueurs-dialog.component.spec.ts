import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CopieMailJoueursDialogComponent } from './copie-mail-joueurs-dialog.component';

describe('CopieMailJoueursDialogComponent', () => {
  let component: CopieMailJoueursDialogComponent;
  let fixture: ComponentFixture<CopieMailJoueursDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CopieMailJoueursDialogComponent]
    });
    fixture = TestBed.createComponent(CopieMailJoueursDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
