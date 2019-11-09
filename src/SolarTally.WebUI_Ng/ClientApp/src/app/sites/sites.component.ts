import { Component, OnInit } from '@angular/core';
import { Site, SiteBrief, SiteBriefsLst } from './shared/site';
import { SITES } from './shared/mock-sites';
import { SiteService } from './shared/site.service';

@Component({
  selector: 'app-sites',
  templateUrl: './sites.component.html',
  styleUrls: ['./sites.component.scss']
})
export class SitesComponent implements OnInit {

  siteBriefs: SiteBrief[];

  constructor(public siteSrvc: SiteService) { }

  ngOnInit() {
    this.getSites();
  }

  getSites(): void {
    this.siteSrvc.getSites()
      .subscribe(siteBriefsLst => {
        this.siteBriefs = siteBriefsLst.items;
      });
  }

}
