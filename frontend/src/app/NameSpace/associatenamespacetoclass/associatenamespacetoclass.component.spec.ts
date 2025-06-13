import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociatenamespacetoclassComponent } from './associatenamespacetoclass.component';

describe('AssociatenamespacetoclassComponent', () => {
  let component: AssociatenamespacetoclassComponent;
  let fixture: ComponentFixture<AssociatenamespacetoclassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssociatenamespacetoclassComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssociatenamespacetoclassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
