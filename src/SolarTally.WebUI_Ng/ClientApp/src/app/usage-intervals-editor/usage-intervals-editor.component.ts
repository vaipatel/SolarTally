import { Component, OnInit, Input, AfterViewInit, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { FormArray, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ApplianceUsageSchedule, ApplianceUsageScheduleStr } from '../shared/dtos/appliance-usage-schedule';
import { TimeSpan } from '../shared/dtos/time-span';
import { UsageTimeInterval, UsageTimeIntervalStr } from '../shared/dtos/usage-time-interval';
import { UsageKind, USAGE_KINDS } from '../shared/enums/usage-kind.enum';

@Component({
  selector: 'app-usage-intervals-editor',
  templateUrl: './usage-intervals-editor.component.html',
  styleUrls: ['./usage-intervals-editor.component.scss']
})
export class UsageIntervalsEditorComponent implements OnInit {

  // @Input() schedule: ApplianceUsageSchedule;
  @Input() schedule: ApplianceUsageScheduleStr;
  @Input() auForm: FormGroup;
  @Output() removeUti: EventEmitter<any> = new EventEmitter();

  usageIntervalsGroup: FormGroup;
  usageKinds: string[] = USAGE_KINDS;
  
  constructor(private fb: FormBuilder) {
  }

  // ngOnInit() {
  //   this.usageIntervalsGroup = this.fb.group({
  //     utis: this.fb.array([])
  //   });
  //   this.usageIntervalsGroup.setParent(this.auForm);

  //   this.schedule.usageIntervals.forEach((ui: UsageTimeInterval) => {
  //     this.addUti(ui.timeInterval.start, ui.timeInterval.end, ui.usageKind);
  //   });
  // }

  ngOnInit() {
    this.usageIntervalsGroup = this.fb.group({
      utis: this.fb.array([])
    });
    this.usageIntervalsGroup.setParent(this.auForm);

    this.schedule.usageIntervals.forEach((ui: UsageTimeIntervalStr) => {
      this.addUti(ui.timeInterval.start, ui.timeInterval.end, ui.usageKind);
    });
  }

  get utis() {
    return this.usageIntervalsGroup.get('utis') as FormArray;
  }

  // addUti(s: TimeSpan, e: TimeSpan, uk: UsageKind) {
  //   this.utis.push(this.fb.group({
  //     start: [TimeSpan.toAString(s), Validators.required],
  //     end: [TimeSpan.toAString(e), Validators.required],
  //     usageKind: [uk, Validators.required]
  //   }));
  // }

  addUti(s: string, e: string, uk: UsageKind) {
    this.utis.push(this.fb.group({
      start: [s, Validators.required],
      end: [e, Validators.required],
      usageKind: [uk, Validators.required]
    }));
  }

  emitRemoveUti(id: number) {
    this.utis.removeAt(id);
    this.removeUti.emit();
  }

}
