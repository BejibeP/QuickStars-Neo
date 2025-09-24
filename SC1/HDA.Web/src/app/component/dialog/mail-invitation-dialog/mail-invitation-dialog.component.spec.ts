import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MailInvitationDialogComponent } from './mail-invitation-dialog.component';

describe('MailInvitationDialogComponent', () => {
  let component: MailInvitationDialogComponent;
  let fixture: ComponentFixture<MailInvitationDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MailInvitationDialogComponent]
    });
    fixture = TestBed.createComponent(MailInvitationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
