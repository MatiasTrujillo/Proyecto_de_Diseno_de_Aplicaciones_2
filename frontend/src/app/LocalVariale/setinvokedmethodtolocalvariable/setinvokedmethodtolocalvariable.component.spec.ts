import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetinvokedmethodtolocalvariableComponent } from './setinvokedmethodtolocalvariable.component';

describe('SetinvokedmethodtolocalvariableComponent', () => {
  let component: SetinvokedmethodtolocalvariableComponent;
  let fixture: ComponentFixture<SetinvokedmethodtolocalvariableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SetinvokedmethodtolocalvariableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SetinvokedmethodtolocalvariableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
