import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateparameterComponent } from './createparameter.component';

describe('CreateparameterComponent', () => {
  let component: CreateparameterComponent;
  let fixture: ComponentFixture<CreateparameterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateparameterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateparameterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
