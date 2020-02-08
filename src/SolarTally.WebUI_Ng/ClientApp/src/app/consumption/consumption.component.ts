import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/services/api.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ApplianceUsageLst, ApplianceUsage, ApplianceUsageLstStr, ApplianceUsageStr } from '../shared/dtos/appliance-usage';
import { HttpErrorResponse } from '@angular/common/http';
import { UpdateApplianceUsageCommandStr } from '../shared/dtos/update-appliance-usage-command';
import { Consumption } from '../shared/dtos/consumption';
import { UsageTimeInterval, UsageTimeIntervalStr } from '../shared/dtos/usage-time-interval';

@Component({
  selector: 'app-consumption',
  templateUrl: './consumption.component.html',
  styleUrls: ['./consumption.component.scss']
})
export class ConsumptionComponent implements OnInit {

  // private auList$: Observable<ApplianceUsageLst>;
  private auListStr$: Observable<ApplianceUsageLstStr>;
  idParam = { id: "" };
  // auList: ApplianceUsage[];
  auListStr: ApplianceUsageStr[];

  constructor(
    private route: ActivatedRoute,
    private api: ApiService) { }

  // ngOnInit() {
  //   // Read route params
  //   this.route.paramMap.pipe(
  //     switchMap((params: ParamMap) => {
  //       this.idParam.id = params.get('id');
  //       this.auList$ =
  //         this.api.getApplianceUsagesForConsumption(this.idParam.id);
  //       return this.auList$;
  //     })
  //   ).subscribe((resp: ApplianceUsageLst) => {
  //     if (!resp) {
  //       console.log("No Appliance Usages founded.");
  //     }
  //     this.auList = resp.items;
  //     console.log(this.auList);
  //   }, (error: HttpErrorResponse) => console.log(
  //     "ApplianceUsages not found - Error " + error.status
  //   ));
  // }
  ngOnInit() {
    // Read route params
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        this.idParam.id = params.get('id');
        this.auListStr$ =
          this.api.getApplianceUsagesForConsumptionStr(this.idParam.id);
          return this.auListStr$;

      })
    ).subscribe((resp: ApplianceUsageLstStr) => {
      if (!resp) {
        console.log("No Appliance Usages founded.");
      }
      this.auListStr = resp.items;
      console.log(this.auListStr);
    }, (error: HttpErrorResponse) => console.log(
      "ApplianceUsages not found - Error " + error.status
    ));
  }

  // addAppliance() {
  //   console.log("Will add appliance");
  //   this.api.mockAddToConsumption(this.idParam.id)
  //   .subscribe(response => {
  //     console.log("Response is:");
  //     console.log(response);
  //     this.auList.push(response);
  //   });
  // }

  removeApplianceUsage(au: ApplianceUsageStr) {
    console.log("Will remove appliance usage " + au.id);
  }

  // updateApplianceUsage(au: ApplianceUsage) {
  //   console.log("Will update appliance usage. Here is the command.");
  //   let command = new UpdateApplianceUsageCommand();
  //   command.consumptionId = +this.idParam.id;
  //   command.applianceUsageId = au.id;
  //   command.quantity = au.quantity;
  //   command.powerConsumption = au.powerConsumption;
  //   command.usageIntervals = new Array<UsageTimeIntervalAbrv>();
  //   au.applianceUsageScheduleDto.usageIntervals.forEach((ui: UsageTimeInterval) => {
  //     var newUTI = new UsageTimeIntervalAbrv();
  //     newUTI.timeInterval = new TimeIntervalAbrv();
  //     newUTI.timeInterval.start = new TimeSpanAbrv();
  //     newUTI.timeInterval.start.hours = ui.timeInterval.start.hours;
  //     newUTI.timeInterval.start.minutes = ui.timeInterval.start.minutes;
  //     newUTI.timeInterval.end = new TimeSpanAbrv();
  //     newUTI.timeInterval.end.hours = ui.timeInterval.end.hours;
  //     newUTI.timeInterval.end.minutes = ui.timeInterval.end.minutes;
  //     command.usageIntervals.push(newUTI);
  //   })
    
  //   console.log(command);
  //   this.api.updateApplianceUsage(command)
  //   .subscribe((response: Consumption) => {
  //     console.log("Response is:");
  //     console.log(response);
  //     // console.log(response.applianceUsages[0]);
  //   });
  // }

  updateApplianceUsage(au: ApplianceUsageStr) {
    console.log("Will update appliance usage. Here is the command.");
    let command = new UpdateApplianceUsageCommandStr();
    command.consumptionId = +this.idParam.id;
    command.applianceUsageId = au.id;
    command.quantity = au.quantity;
    command.powerConsumption = au.powerConsumption;
    command.usageIntervals = new Array<UsageTimeIntervalStr>();
    au.applianceUsageScheduleDto.usageIntervals.forEach((ui: UsageTimeIntervalStr) => {
      var newUTI = new UsageTimeIntervalStr();
      newUTI.timeInterval = ui.timeInterval;
      command.usageIntervals.push(newUTI);
    });
    
    console.log(command);
    this.api.updateApplianceUsage(command)
    .subscribe((response: Consumption) => {
      console.log("Response is:");
      console.log(response);
      // console.log(response.applianceUsages[0]);
    });
  }

}
