import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociateattributewithclassComponent } from './associateattributewithclass.component';

describe('AssociateattributewithclassComponent', () => {
  let component: AssociateattributewithclassComponent;
  let fixture: ComponentFixture<AssociateattributewithclassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssociateattributewithclassComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssociateattributewithclassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
