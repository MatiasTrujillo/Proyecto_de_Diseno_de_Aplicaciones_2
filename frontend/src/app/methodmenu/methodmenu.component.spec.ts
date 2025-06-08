import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MethodmenuComponent } from './methodmenu.component';

describe('MethodmenuComponent', () => {
  let component: MethodmenuComponent;
  let fixture: ComponentFixture<MethodmenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MethodmenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MethodmenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
