import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Site } from '../shared/dtos/site';
import { SiteService } from '../shared/services/site.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-site-detail',
  templateUrl: './site-detail.component.html',
  styleUrls: ['./site-detail.component.scss']
})
export class SiteDetailComponent implements OnInit {

  private site$: Observable<Site>;
  site: Site;

  constructor(
    public route: ActivatedRoute,
    private router: Router,
    private siteSrvc: SiteService
  ) { }

  ngOnInit() {
    // Read route params
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.site$ = this.siteSrvc.getSiteDetail(params.get('id')))
    ).subscribe((resp: Site) => {
      if (!resp) {
        console.log("Site not found.");
      }
      this.site = resp;
    }, (error: HttpErrorResponse) => console.log(
      "Site not found - Error " + error.status
    ));
  }

}
