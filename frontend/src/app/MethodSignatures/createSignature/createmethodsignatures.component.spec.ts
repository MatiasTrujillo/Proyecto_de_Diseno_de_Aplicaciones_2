import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateMethodSignaturesComponent } from './createmethodsignatures.component';

describe('MethodSignaturesComponent', () => {
  let component: CreateMethodSignaturesComponent;
  let fixture: ComponentFixture<CreateMethodSignaturesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateMethodSignaturesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateMethodSignaturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
