import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatenamespaceComponent } from './createnamespace.component';

describe('CreatenamespaceComponent', () => {
  let component: CreatenamespaceComponent;
  let fixture: ComponentFixture<CreatenamespaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreatenamespaceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatenamespaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
