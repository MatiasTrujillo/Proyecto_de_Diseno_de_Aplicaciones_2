import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddmethodComponent } from './addmethod.component';

describe('AddmethodComponent', () => {
  let component: AddmethodComponent;
  let fixture: ComponentFixture<AddmethodComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddmethodComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddmethodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
