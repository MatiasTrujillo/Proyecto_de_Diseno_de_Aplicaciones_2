import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateinvokedmethodComponent } from './createinvokedmethod.component';

describe('CreateinvokedmethodComponent', () => {
  let component: CreateinvokedmethodComponent;
  let fixture: ComponentFixture<CreateinvokedmethodComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateinvokedmethodComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateinvokedmethodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
