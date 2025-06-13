import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFatherToNameSpaceComponent } from './add-father-to-name-space.component';

describe('AddFatherToNameSpaceComponent', () => {
  let component: AddFatherToNameSpaceComponent;
  let fixture: ComponentFixture<AddFatherToNameSpaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddFatherToNameSpaceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddFatherToNameSpaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
