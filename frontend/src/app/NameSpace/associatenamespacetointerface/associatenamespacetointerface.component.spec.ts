import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociatenamespacetointerfaceComponent } from './associatenamespacetointerface.component';

describe('AssociatenamespacetointerfaceComponent', () => {
  let component: AssociatenamespacetointerfaceComponent;
  let fixture: ComponentFixture<AssociatenamespacetointerfaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssociatenamespacetointerfaceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssociatenamespacetointerfaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
