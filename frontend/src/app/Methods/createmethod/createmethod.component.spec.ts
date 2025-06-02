import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatemethodComponent } from './createmethod.component';

describe('CreatemethodComponent', () => {
  let component: CreatemethodComponent;
  let fixture: ComponentFixture<CreatemethodComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatemethodComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatemethodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
