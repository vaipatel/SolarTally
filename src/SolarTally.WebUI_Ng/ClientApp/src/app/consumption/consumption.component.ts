import { Component, OnInit } from '@angular/core';
import { ConsumptionService } from './shared/consumption.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ApplianceUsageLst, ApplianceUsage } from '../shared/appliance-usage';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-consumption',
  templateUrl: './consumption.component.html',
  styleUrls: ['./consumption.component.scss']
})
export class ConsumptionComponent implements OnInit {

  private auList$: Observable<ApplianceUsageLst>;
  idParam = { id: "" };
  auList: ApplianceUsage[];

  constructor(
    private route: ActivatedRoute,
    private consumptionSrvc: ConsumptionService) { }

  ngOnInit() {
    // Read route params
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        this.idParam.id = params.get('id');
        this.auList$ = this.consumptionSrvc
          .getApplianceUsagesForConsumption(this.idParam.id)
        return this.auList$;
      })
    ).subscribe((resp: ApplianceUsageLst) => {
      if (!resp) {
        console.log("No Appliance Usages founded.");
      }
      this.auList = resp.items;
    }, (error: HttpErrorResponse) => console.log(
      "ApplianceUsages not found - Error " + error.status
    ));
  }

  addAppliance() {
    console.log("Will add appliance");
    this.consumptionSrvc.mockAddToConsumption(this.idParam.id)
    .subscribe(response => {
      console.log("Response is:");
      console.log(response);
    });
  }

}
