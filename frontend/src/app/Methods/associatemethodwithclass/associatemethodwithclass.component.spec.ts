import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociatemethodwithclassComponent } from './associatemethodwithclass.component';

describe('AssociatemethodwithclassComponent', () => {
  let component: AssociatemethodwithclassComponent;
  let fixture: ComponentFixture<AssociatemethodwithclassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssociatemethodwithclassComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssociatemethodwithclassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
