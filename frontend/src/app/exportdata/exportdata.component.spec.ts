import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExportdataComponent } from './exportdata.component';

describe('ExportdataComponent', () => {
  let component: ExportdataComponent;
  let fixture: ComponentFixture<ExportdataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExportdataComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExportdataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
