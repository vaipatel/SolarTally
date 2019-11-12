import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Site, SiteBriefsLst } from '../dtos/site';
import { ApplianceUsageLst, ApplianceUsage } from '../dtos/appliance-usage';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private sitesUrl = "api/sites/getall";
  private siteDetailUrl = "api/sites/getdetail/";
  private auByIdUrl = "api/applianceusages/getforconsumption/";
  private addAUByIdUrl = "api/applianceusages/addtoconsumption/";

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
}
