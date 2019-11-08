import { Component, OnInit } from '@angular/core';
import { Site } from './shared/site';
import { SITES } from './shared/mock-sites';

@Component({
  selector: 'app-sites',
  templateUrl: './sites.component.html',
  styleUrls: ['./sites.component.scss']
})
export class SitesComponent implements OnInit {

  sites: Site[] = SITES;

  constructor() { }

  ngOnInit() {
  }

}
