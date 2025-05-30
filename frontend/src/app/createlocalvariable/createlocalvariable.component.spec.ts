import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatelocalvariableComponent } from './createlocalvariable.component';

describe('CreatelocalvariableComponent', () => {
  let component: CreatelocalvariableComponent;
  let fixture: ComponentFixture<CreatelocalvariableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatelocalvariableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatelocalvariableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
