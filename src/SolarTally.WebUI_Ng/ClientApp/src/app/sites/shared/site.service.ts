import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Site, SiteBrief, SiteBriefsLst } from './site';

@Injectable({
  providedIn: 'root'
})
export class SiteService {

  private sitesUrl = "api/sites/getall";
  private siteDetailUrl = "api/sites/getdetail/";

  constructor(private http: HttpClient) { }

  getSites(): Observable<SiteBriefsLst> {
    return this.http.get<SiteBriefsLst>(this.sitesUrl);
  }

  getSiteDetail(id: string): Observable<Site> {
    return this.http.get<Site>(this.siteDetailUrl + id);
  }
}
