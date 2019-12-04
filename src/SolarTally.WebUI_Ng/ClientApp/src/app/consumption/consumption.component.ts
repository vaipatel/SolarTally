import { Component, OnInit } from '@angular/core';
import { ApiService } from '../shared/services/api.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ApplianceUsageLst, ApplianceUsage, ApplianceUsageLstStr, ApplianceUsageStr } from '../shared/dtos/appliance-usage';
import { HttpErrorResponse } from '@angular/common/http';

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

}
