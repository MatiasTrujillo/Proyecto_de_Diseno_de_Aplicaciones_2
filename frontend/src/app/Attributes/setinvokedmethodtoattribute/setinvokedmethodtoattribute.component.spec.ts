import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetinvokedmethodtoattributeComponent } from './setinvokedmethodtoattribute.component';

describe('SetinvokedmethodtoattributeComponent', () => {
  let component: SetinvokedmethodtoattributeComponent;
  let fixture: ComponentFixture<SetinvokedmethodtoattributeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SetinvokedmethodtoattributeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SetinvokedmethodtoattributeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
