import { Component, OnInit, Input, AfterViewInit, OnChanges, SimpleChanges } from '@angular/core';
import { FormArray, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ApplianceUsageSchedule } from '../shared/dtos/appliance-usage-schedule';
import { TimeSpan } from '../shared/dtos/time-span';
import { UsageTimeInterval } from '../shared/dtos/usage-time-interval';

@Component({
  selector: 'app-usage-intervals-editor',
  templateUrl: './usage-intervals-editor.component.html',
  styleUrls: ['./usage-intervals-editor.component.scss']
})
export class UsageIntervalsEditorComponent implements OnInit {

  @Input() schedule: ApplianceUsageSchedule;
  @Input() auForm: FormGroup;

  usageIntervalsGroup: FormGroup;
  
  constructor(private fb: FormBuilder) {
  }

  ngOnInit() {
    this.usageIntervalsGroup = this.fb.group({
      utis: this.fb.array([])
    });
    this.usageIntervalsGroup.setParent(this.auForm);

    this.schedule.usageIntervals.forEach((ui: UsageTimeInterval) => {
      this.addUti(ui.timeInterval.start, ui.timeInterval.end);
    });
  }

  // ngOnChanges(changes: SimpleChanges) {
  //   for (let propName in changes) {
  //     let chng = changes[propName];
  //     let cur  = JSON.stringify(chng.currentValue);
  //     let prev = JSON.stringify(chng.previousValue);
  //     console.log(`${propName}: currentValue = ${cur}, previousValue = ${prev}`);
  //   }
  // }

  get utis() {
    return this.usageIntervalsGroup.get('utis') as FormArray;
  }

  addUti(s: TimeSpan, e: TimeSpan) {
    // console.log(s.toString());
    this.utis.push(this.fb.group({
      start: [TimeSpan.toAString(s), Validators.required],
      end: [TimeSpan.toAString(e), Validators.required]
    }));
  }

}
