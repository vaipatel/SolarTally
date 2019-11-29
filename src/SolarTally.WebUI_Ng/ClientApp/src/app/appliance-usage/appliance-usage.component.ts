import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormArray, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ApplianceUsage } from '../shared/dtos/appliance-usage';

@Component({
  selector: 'app-appliance-usage',
  templateUrl: './appliance-usage.component.html',
  styleUrls: ['./appliance-usage.component.scss']
})
export class ApplianceUsageComponent implements OnInit {

  @Input() au: ApplianceUsage;
  @Output() remove: EventEmitter<any> = new EventEmitter();
  @Output() update: EventEmitter<any> = new EventEmitter();

  auForm: FormGroup = this.fb.group({});

  constructor(public fb: FormBuilder) { 
  }

  ngOnInit() {
  }

  emitRemove() {
    this.remove.emit(this.au);
  }

  emitUpdate() {
    this.update.emit(this.au);
  }

}
