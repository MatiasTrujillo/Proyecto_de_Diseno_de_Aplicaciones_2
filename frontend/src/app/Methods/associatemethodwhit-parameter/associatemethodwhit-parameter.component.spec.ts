import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociatemethodwhitParameterComponent } from './associatemethodwhit-parameter.component';

describe('AssociatemethodwhitParameterComponent', () => {
  let component: AssociatemethodwhitParameterComponent;
  let fixture: ComponentFixture<AssociatemethodwhitParameterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssociatemethodwhitParameterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssociatemethodwhitParameterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
