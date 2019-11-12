import { Component, OnInit } from '@angular/core';
import { Site, SiteBrief, SiteBriefsLst } from '../shared/dtos/site';
import { SITES } from '../shared/services/mock-sites';
import { ApiService } from '../shared/services/api.service';
import { MatTableDataSource, MatTab } from '@angular/material';

@Component({
  selector: 'app-sites',
  templateUrl: './sites.component.html',
  styleUrls: ['./sites.component.scss']
})
export class SitesComponent implements OnInit {

  siteBriefs: SiteBrief[];
  dataSource: MatTableDataSource<SiteBrief>;
  displayedColumns: string[] = [
    "name", "totalPowerConsumption", "totalEnergyConsumption", "city", "to_detail_arrow"
  ];

  constructor(public api: ApiService) { }

  ngOnInit() {
    this.getSites();
  }

  getSites(): void {
    this.api.getSites()
      .subscribe(siteBriefsLst => {
        this.siteBriefs = siteBriefsLst.items;
        this.dataSource = new MatTableDataSource(this.siteBriefs);
      });
  }

}
