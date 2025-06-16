import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemovechildComponent } from './removechild.component';

describe('RemovechildComponent', () => {
  let component: RemovechildComponent;
  let fixture: ComponentFixture<RemovechildComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RemovechildComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RemovechildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
