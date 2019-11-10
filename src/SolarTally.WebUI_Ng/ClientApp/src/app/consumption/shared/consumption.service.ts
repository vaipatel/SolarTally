import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ApplianceUsageLst } from 'src/app/shared/appliance-usage';

@Injectable({
  providedIn: 'root'
})
export class ConsumptionService {
  
  private auByIdUrl = "api/applianceusages/getforconsumption/";

  constructor(private http: HttpClient) { }

  getApplianceUsagesForConsumption(id: string): Observable<ApplianceUsageLst>{
    return this.http.get<ApplianceUsageLst>(this.auByIdUrl + id);
  }
}
