import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttributemenuComponent } from './attributemenu.component';

describe('AttributemenuComponent', () => {
  let component: AttributemenuComponent;
  let fixture: ComponentFixture<AttributemenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AttributemenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AttributemenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
