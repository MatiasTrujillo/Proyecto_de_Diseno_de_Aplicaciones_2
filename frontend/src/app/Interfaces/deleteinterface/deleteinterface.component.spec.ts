import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteinterfaceComponent } from './deleteinterface.component';

describe('DeleteInterfaceComponent', () => {
  let component: DeleteinterfaceComponent;
  let fixture: ComponentFixture<DeleteinterfaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteinterfaceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteinterfaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
