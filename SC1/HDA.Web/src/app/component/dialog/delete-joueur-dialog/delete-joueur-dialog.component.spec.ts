import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteJoueurDialogComponent } from './delete-joueur-dialog.component';

describe('DeleteJoueurDialogComponent', () => {
  let component: DeleteJoueurDialogComponent;
  let fixture: ComponentFixture<DeleteJoueurDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DeleteJoueurDialogComponent]
    });
    fixture = TestBed.createComponent(DeleteJoueurDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
