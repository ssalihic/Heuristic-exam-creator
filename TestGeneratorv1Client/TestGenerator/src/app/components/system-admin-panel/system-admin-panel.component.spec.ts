import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemAdminPanelComponent } from './system-admin-panel.component';

describe('SystemAdminPanelComponent', () => {
  let component: SystemAdminPanelComponent;
  let fixture: ComponentFixture<SystemAdminPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SystemAdminPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemAdminPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
