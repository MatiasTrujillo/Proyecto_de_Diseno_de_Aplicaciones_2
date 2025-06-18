import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteattributeComponent } from './deleteattribute.component';

describe('DeleteattributeComponent', () => {
  let component: DeleteattributeComponent;
  let fixture: ComponentFixture<DeleteattributeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteattributeComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteattributeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
