import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetInvokeMethodToParameterComponent } from './set-invoke-method-to-parameter.component';

describe('SetInvokeMethodToParameterComponent', () => {
  let component: SetInvokeMethodToParameterComponent;
  let fixture: ComponentFixture<SetInvokeMethodToParameterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SetInvokeMethodToParameterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SetInvokeMethodToParameterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
