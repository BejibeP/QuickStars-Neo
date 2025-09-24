import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyRepasDialogComponent } from './modify-repas-dialog.component';

describe('ModifyRepasDialogComponent', () => {
  let component: ModifyRepasDialogComponent;
  let fixture: ComponentFixture<ModifyRepasDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModifyRepasDialogComponent]
    });
    fixture = TestBed.createComponent(ModifyRepasDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
