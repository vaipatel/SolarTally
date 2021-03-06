import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Site, SiteBriefsLst } from '../dtos/site';
import { ApplianceUsageLst, ApplianceUsage, ApplianceUsageLstStr } from '../dtos/appliance-usage';
import { Consumption } from '../dtos/consumption';
import { UpdateApplianceUsageCommandStr } from '../dtos/update-appliance-usage-command';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private sitesUrl = "api/sites/getall";
  private siteDetailUrl = "api/sites/getdetail/";
  private auByIdUrl = "api/applianceusages/getforconsumption/";
  private addAUByIdUrl = "api/applianceusages/addtoconsumption/";
  private updateAUUrl = "api/applianceusages/updateapplianceusage";

  constructor(private http: HttpClient) { }

  getSites(): Observable<SiteBriefsLst> {
    return this.http.get<SiteBriefsLst>(this.sitesUrl);
  }

  getSiteDetail(id: string): Observable<Site> {
    return this.http.get<Site>(this.siteDetailUrl + id);
  }

  getApplianceUsagesForConsumption(id: string): Observable<ApplianceUsageLst>{
    return this.http.get<ApplianceUsageLst>(this.auByIdUrl + id);
  }

  getApplianceUsagesForConsumptionStr(id: string): Observable<ApplianceUsageLstStr>{
    return this.http.get<ApplianceUsageLstStr>(this.auByIdUrl + id);
  }

  mockAddToConsumption(id: string): Observable<ApplianceUsage> {
    return this.http.put<ApplianceUsage>(this.addAUByIdUrl + id, {
      consumptionId: +id,
      applianceId: 2,
      quantity: 2,
      powerConsumption: 30,
      numHoursOnSolar: 1,
      numHoursOffSolar: 1
    });
  }

  updateApplianceUsage(command: UpdateApplianceUsageCommandStr): Observable<Consumption> {
    return this.http.post<Consumption>(this.updateAUUrl, command);
  }
}
