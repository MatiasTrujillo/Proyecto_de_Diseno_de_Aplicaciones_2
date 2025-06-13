import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParametermenuComponent } from './parametermenu.component';

describe('ParametermenuComponent', () => {
  let component: ParametermenuComponent;
  let fixture: ComponentFixture<ParametermenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ParametermenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParametermenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
