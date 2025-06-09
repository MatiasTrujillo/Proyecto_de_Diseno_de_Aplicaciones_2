import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NameSpaceMenuComponent } from './name-space-menu.component';

describe('NameSpaceMenuComponent', () => {
  let component: NameSpaceMenuComponent;
  let fixture: ComponentFixture<NameSpaceMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NameSpaceMenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NameSpaceMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
