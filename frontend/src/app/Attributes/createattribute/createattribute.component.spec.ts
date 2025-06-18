import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateattributeComponent } from './createattribute.component';

describe('CreateattributeComponent', () => {
  let component: CreateattributeComponent;
  let fixture: ComponentFixture<CreateattributeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateattributeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateattributeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
