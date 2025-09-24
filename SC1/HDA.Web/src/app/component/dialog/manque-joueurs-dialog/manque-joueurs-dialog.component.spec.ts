import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManqueJoueursDialogComponent } from './manque-joueurs-dialog.component';

describe('ManqueJoueursDialogComponent', () => {
  let component: ManqueJoueursDialogComponent;
  let fixture: ComponentFixture<ManqueJoueursDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManqueJoueursDialogComponent]
    });
    fixture = TestBed.createComponent(ManqueJoueursDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
