import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsageIntervalsEditorComponent } from './usage-intervals-editor.component';

describe('UsageIntervalsEditorComponent', () => {
  let component: UsageIntervalsEditorComponent;
  let fixture: ComponentFixture<UsageIntervalsEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsageIntervalsEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsageIntervalsEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
