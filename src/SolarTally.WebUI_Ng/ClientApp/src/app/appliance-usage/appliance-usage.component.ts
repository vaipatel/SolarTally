import { Component, OnInit, Input } from '@angular/core';
import { ApplianceUsage } from '../shared/dtos/appliance-usage';

@Component({
  selector: 'app-appliance-usage',
  templateUrl: './appliance-usage.component.html',
  styleUrls: ['./appliance-usage.component.scss']
})
export class ApplianceUsageComponent implements OnInit {

  @Input() au: ApplianceUsage;

  constructor() { }

  ngOnInit() {
  }

}
