import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SitesComponent } from './sites/sites.component';
import { SiteDetailComponent } from './sites/site-detail/site-detail.component';

const routes: Routes = [
  {path: 'sites', component: SitesComponent},
  {path: 'site-detail', component: SiteDetailComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
