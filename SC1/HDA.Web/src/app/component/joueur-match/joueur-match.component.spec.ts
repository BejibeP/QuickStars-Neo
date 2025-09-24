import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JoueurMatchComponent } from './joueur-match.component';

describe('AddJoueurComponent', () => {
  let component: JoueurMatchComponent;
  let fixture: ComponentFixture<JoueurMatchComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [JoueurMatchComponent]
    });
    fixture = TestBed.createComponent(JoueurMatchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
