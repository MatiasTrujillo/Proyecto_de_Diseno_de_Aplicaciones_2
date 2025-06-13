import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociateInterfaceSignatureComponent } from './associate-interface-signature.component';

describe('AssociateInterfaceSignatureComponent', () => {
  let component: AssociateInterfaceSignatureComponent;
  let fixture: ComponentFixture<AssociateInterfaceSignatureComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssociateInterfaceSignatureComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssociateInterfaceSignatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
