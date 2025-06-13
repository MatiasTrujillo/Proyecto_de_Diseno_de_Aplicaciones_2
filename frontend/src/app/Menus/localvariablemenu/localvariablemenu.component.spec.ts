import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocalvariablemenuComponent } from './localvariablemenu.component';

describe('LocalvariablemenuComponent', () => {
  let component: LocalvariablemenuComponent;
  let fixture: ComponentFixture<LocalvariablemenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LocalvariablemenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LocalvariablemenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
