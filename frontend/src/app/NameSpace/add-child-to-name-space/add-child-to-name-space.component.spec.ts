import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddChildToNameSpaceComponent } from './add-child-to-name-space.component';

describe('AddChildToNameSpaceComponent', () => {
  let component: AddChildToNameSpaceComponent;
  let fixture: ComponentFixture<AddChildToNameSpaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddChildToNameSpaceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddChildToNameSpaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
