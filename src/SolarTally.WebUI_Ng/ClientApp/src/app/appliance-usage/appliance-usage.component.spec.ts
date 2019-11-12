import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplianceUsageComponent } from './appliance-usage.component';

describe('ApplianceUsageComponent', () => {
  let component: ApplianceUsageComponent;
  let fixture: ComponentFixture<ApplianceUsageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApplianceUsageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplianceUsageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
