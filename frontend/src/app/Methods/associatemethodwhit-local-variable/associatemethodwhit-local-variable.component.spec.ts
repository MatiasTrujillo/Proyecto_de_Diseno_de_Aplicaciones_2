import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociatemethodwhitLocalVariableComponent } from './associatemethodwhit-local-variable.component';

describe('AssociatemethodwhitLocalVariableComponent', () => {
  let component: AssociatemethodwhitLocalVariableComponent;
  let fixture: ComponentFixture<AssociatemethodwhitLocalVariableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssociatemethodwhitLocalVariableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssociatemethodwhitLocalVariableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
