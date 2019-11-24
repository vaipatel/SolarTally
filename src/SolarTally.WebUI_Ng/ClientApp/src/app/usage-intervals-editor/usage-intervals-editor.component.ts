import { Component, OnInit, Input } from '@angular/core';
import { FormArray, FormGroup, FormBuilder } from '@angular/forms';
import { ApplianceUsageSchedule } from '../shared/dtos/appliance-usage-schedule';

@Component({
  selector: 'app-usage-intervals-editor',
  templateUrl: './usage-intervals-editor.component.html',
  styleUrls: ['./usage-intervals-editor.component.scss']
})
export class UsageIntervalsEditorComponent implements OnInit {

  @Input() schedule: ApplianceUsageSchedule;

  usageIntervalsForm: FormGroup = this.fb.group({
    utis: this.fb.array([
      this.fb.group({
        start: [''],
        end: ['']
      })
    ])
  })
  
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
  }

  get utis() {
    return this.usageIntervalsForm.get('utis') as FormArray;
  }

  addUti() {
    this.utis.push(this.fb.group({
      start: [''],
      end: ['']
    }));
  }

}
