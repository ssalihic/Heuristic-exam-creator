import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestPrintPreviewComponent } from './test-print-preview.component';

describe('TestPrintPreviewComponent', () => {
  let component: TestPrintPreviewComponent;
  let fixture: ComponentFixture<TestPrintPreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestPrintPreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestPrintPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
