import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module'; 

import { AppComponent } from './app.component';
import { StoreModule } from '@ngrx/store';
import { LoginReducer } from './reducers';
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
    BrowserModule,FormsModule,AppRoutingModule,
    StoreModule.forRoot({login : LoginReducer}),
    //EffectsModule.forRoot([AppEffects])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
