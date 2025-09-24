import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MailParticipantsDialogComponent } from './mail-participants-dialog.component';

describe('MailParticipantsDialogComponent', () => {
  let component: MailParticipantsDialogComponent;
  let fixture: ComponentFixture<MailParticipantsDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MailParticipantsDialogComponent]
    });
    fixture = TestBed.createComponent(MailParticipantsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
