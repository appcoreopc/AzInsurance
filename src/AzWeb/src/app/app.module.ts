import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { StoreModule } from '@ngrx/store';
import { reducers, metaReducers } from './reducers';
import { EffectsModule } from '@ngrx/effects';
import { AppEffects } from './app.effects';
import { CarInsuranceComponent } from './static/car-insurance/car-insurance.component';
import { HouseInsuranceComponent } from './static/house-insurance/house-insurance.component';
import { HealthInsuranceComponent } from './static/health-insurance/health-insurance.component';

@NgModule({
  declarations: [
    AppComponent,
    CarInsuranceComponent,
    HouseInsuranceComponent,
    HealthInsuranceComponent,
    ],
  imports: [
    BrowserModule,FormsModule,
    StoreModule.forRoot(reducers, { metaReducers }),
    EffectsModule.forRoot([AppEffects])
  ],
  providers: [],
  bootstrap: [AppComponent,]
})
export class AppModule { }
