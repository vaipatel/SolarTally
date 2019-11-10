import { Component, OnInit } from '@angular/core';
import { ConsumptionService } from './shared/consumption.service';

@Component({
  selector: 'app-consumption',
  templateUrl: './consumption.component.html',
  styleUrls: ['./consumption.component.scss']
})
export class ConsumptionComponent implements OnInit {

  constructor(private consumptionSrvc: ConsumptionService) { }

  ngOnInit() {
  }

}
