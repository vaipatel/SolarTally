import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule }    from '@angular/common/http';
import { DecimalPipe } from '@angular/common';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SitesComponent } from './sites/sites.component';
import { SiteDetailComponent } from './site-detail/site-detail.component';
import { ConsumptionComponent } from './consumption/consumption.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// Material
import {
  MatButtonModule,
  MatCardModule,
  MatExpansionModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatTableModule,
  MatToolbarModule,
  MatTooltipModule
} from '@angular/material';

import { KiloPipe } from './shared/pipes/kilo.pipe';

@NgModule({
  declarations: [
    AppComponent,
    SitesComponent,
    NavMenuComponent,
    SiteDetailComponent,
    ConsumptionComponent,
    KiloPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    //Material
    MatButtonModule,
    MatCardModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatTableModule,
    MatToolbarModule,
    MatTooltipModule
  ],
  providers: [DecimalPipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
