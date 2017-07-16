import * as Raven from 'raven-js';
import { NgModule, ErrorHandler } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { ToastyModule } from 'ng2-toasty';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { VehicleFormComponent } from "./components/vehicle-form/vehicle-form.component";
import { VehicleEditFormComponent } from "./components/vehicle-edit-form/vehicle-edit-form.component";
import { VehicleService } from "./services/vehicle.service";
import { AppErrorHandler } from './app.error-handler';

Raven.config('https://e72f233da43c46b7a41726b28d47963c@sentry.io/191290').install();
export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        VehicleFormComponent,
        VehicleEditFormComponent
        ],
    imports: [
        FormsModule,
        ToastyModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'vehicles/new', component: VehicleFormComponent },
            { path: 'vehicles/:id', component: VehicleEditFormComponent },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
    //with below syntax, we are telling that whereever we need instance of ErrorHandler class, use AppErrorHandler instead
       // { provide: ErrorHandler, useClass: AppErrorHandler},
        VehicleService
    ] 
};
