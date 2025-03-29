import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddExportComponent } from './add-export.component';

describe('AddExportComponent', () => {
  let component: AddExportComponent;
  let fixture: ComponentFixture<AddExportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddExportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddExportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
