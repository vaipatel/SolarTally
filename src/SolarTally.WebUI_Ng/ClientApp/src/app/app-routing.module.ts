import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SitesComponent } from './sites/sites.component';
import { SiteDetailComponent } from './sites/site-detail/site-detail.component';
import { ConsumptionComponent } from './consumption/consumption.component';

const routes: Routes = [
  {path: 'sites', component: SitesComponent},
  {path: 'site-detail', component: SiteDetailComponent},
  {path: 'consumption', component: ConsumptionComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
