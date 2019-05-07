import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PreviewTestsComponent } from './preview-tests.component';

describe('PreviewTestsComponent', () => {
  let component: PreviewTestsComponent;
  let fixture: ComponentFixture<PreviewTestsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PreviewTestsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PreviewTestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
