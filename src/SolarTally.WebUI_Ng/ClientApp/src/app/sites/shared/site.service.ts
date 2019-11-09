import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Site, SiteBrief, SiteBriefLst } from './site';

@Injectable({
  providedIn: 'root'
})
export class SiteService {

  private sitesUrl = "api/sites/getall";

  constructor(private http: HttpClient) { }

  getSites(): Observable<SiteBriefLst> {
    return this.http.get<SiteBriefLst>(this.sitesUrl);
  }
}
