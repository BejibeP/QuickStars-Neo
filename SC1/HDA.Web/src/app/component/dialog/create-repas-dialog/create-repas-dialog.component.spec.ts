import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateRepasDialogComponent } from './create-repas-dialog.component';

describe('CreateRepasDialogComponent', () => {
  let component: CreateRepasDialogComponent;
  let fixture: ComponentFixture<CreateRepasDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateRepasDialogComponent]
    });
    fixture = TestBed.createComponent(CreateRepasDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
