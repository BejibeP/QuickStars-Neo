import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteRepasDialogComponent } from './delete-repas-dialog.component';

describe('DeleteRepasDialogComponent', () => {
  let component: DeleteRepasDialogComponent;
  let fixture: ComponentFixture<DeleteRepasDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteRepasDialogComponent]
    });
    fixture = TestBed.createComponent(DeleteRepasDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
