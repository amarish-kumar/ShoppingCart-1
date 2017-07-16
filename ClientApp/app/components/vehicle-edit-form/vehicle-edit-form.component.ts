import * as _ from 'underscore';
import * as Raven from 'raven-js';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from "../../services/vehicle.service";
import { ActivatedRoute, Router } from '@angular/router';
import { ToastyService } from "ng2-toasty";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/Observable/forkJoin';
import { SaveVehicle, Vehicle } from './../../models/vehicle';
import { isDevMode } from '@angular/core';

@Component({
    selector: 'app-vehicle-edit-form',
    templateUrl: './vehicle-edit-form.component.html',
    styleUrls: ['./vehicle-edit-form.component.css'],
    providers: [VehicleService]
})
export class VehicleEditFormComponent implements OnInit {

    makes: any[];
    models: any[];
    features: any[];
    vehicle: SaveVehicle = {
        id: 0,
        makeId: 0,
        modelId: 0,
        isRegistered: false,
        features: [],
        contact: {
            name: '',
            email: '',
            phone:''
        }
    };
    
    constructor(private route: ActivatedRoute,
        private router: Router,
        private vehicleService: VehicleService,
        private toastyService: ToastyService) {

        route.params.subscribe(p => {
            this.vehicle.id = +p['id'];
           /* if (this.vehicle.id == NaN) {
                this.vehicle.id = 1;
            }*/
            console.log("vehicle id:- ", this.vehicle.id);
        });
    }

    ngOnInit() {
        //DataSources
        var sources = [
            this.vehicleService.getMakes(),
            this.vehicleService.getFeatures(),
        ];

        /*TODO:- when route is /new then vehicle id is going NaN, hence to handle the same, just passed 1 as temp*/
        if (this.vehicle.id) {
            /*    this.vehicleService.getVehicle(this.vehicle.id)*/
            sources.push(this.vehicleService.getVehicle(this.vehicle.id));
        }
        //Here, we can pass multiple observables, order doesn't matter as everything will go in parallel
        Observable.forkJoin(sources).subscribe(data => {
            //Once, all the observables are complete, we'll get data
            this.makes = data[0];
            this.features = data[1];
            if (this.vehicle.id){
                this.setVehicle(data[2]);
                this.populateModels();
            }
           // this.vehicle = data[2];

        },err => {
            if (err.status == 404)
                this.router.navigate(['/home']);
        });
        /* if (this.vehicle.id) {
             console.log("id", this.vehicle.id);
         this.vehicleService.getVehicle(this.vehicle.id)
             .subscribe(v => {
                 this.vehicle = v;
                 });
         }*/
        /*  this.vehicleService.getMakes().subscribe(makes => {
              this.makes = makes;
              console.log("Makes:-", this.makes);
          });*/
        /* this.vehicleService.getFeatures().subscribe(features =>
             this.features = features);*/

    }

    //That's why its good to have interfaces for getting intellisense
    private setVehicle(v:Vehicle) {
        this.vehicle.id = v.id;
        this.vehicle.makeId = v.make.id;
        this.vehicle.modelId = v.model.id;
        this.vehicle.isRegistered = v.isRegistered;
        this.vehicle.contact = v.contact;
        this.vehicle.features = _.pluck(v.features, 'id');
    }

    onMakeChange() {
        this.populateModels();
        console.log("Vehicle", this.vehicle);
        delete this.vehicle.modelId;
    }

    private populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }

    onFeatureToggle(featureId,$event) {
        if ($event.target.checked)
            this.vehicle.features.push(featureId);
        else {
            var index = this.vehicle.features.indexOf(featureId);
            this.vehicle.features.splice(index, 1);
        }
    }
    submit() {
        if (this.vehicle.id) {
            this.vehicleService.updateVehicle(this.vehicle)
                .subscribe(x => {
                        console.log(x);
                        this.toastyService.success({
                            title: 'Success',
                            msg: 'Vehicle Updated!',
                            theme: 'bootstrap',
                            showClose: true,
                            timeout: 5000
                        });
                    },
                    err => {
                        Raven.captureException(err.originalError || err);
                        this.toastyService.error({
                            title: 'Error',
                            msg: 'An unexpected error while updating the record!',
                            theme: 'bootstrap',
                            showClose: true,
                            timeout: 5000
                        });
                    });
        }
        else {
            this.vehicleService.create(this.vehicle)
                //Success
                .subscribe(x => {
                        console.log(x);
                        this.toastyService.success({
                            title: 'Success',
                            msg: 'Vehicle Created!',
                            theme: 'bootstrap',
                            showClose: true,
                            timeout: 5000
                        });
                    },
                    //Error
                    err => {
                        Raven.captureException(err.originalError || err);
                        this.toastyService.error({
                            title: 'Error',
                            msg: 'An unexpected error happend!',
                            theme: 'bootstrap',
                            showClose: true,
                            timeout: 5000
                        });
                    });
        }

    }

}
