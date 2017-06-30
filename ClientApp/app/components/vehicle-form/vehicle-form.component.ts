import { Component, OnInit } from '@angular/core';
import { MakeService } from "../../services/make.service";
import Featureservice = require("../../services/feature.service");
import FeatureService = Featureservice.FeatureService;

@Component({
    selector: 'app-vehicle-form',
    templateUrl: './vehicle-form.component.html',
    styleUrls: ['./vehicle-form.component.css'],
    providers: [MakeService,FeatureService]
})
export class VehicleFormComponent implements OnInit {

    makes: any[];
    models: any[];
    features: any[];
    vehicle: any = {};
    
    constructor(private makeService: MakeService, private featureService:FeatureService) { }

    ngOnInit() {
        this.makeService.getMakes().subscribe(makes => {
            this.makes = makes;
            console.log("Makes:-", this.makes);
        });

        this.featureService.getFeatures().subscribe(features =>
            this.features = features);

    }

    onMakeChange() {
        console.log("Vehicle", this.vehicle);
        var selectedMake = this.makes.find(m => m.id == this.vehicle.make);
        this.models = selectedMake ? selectedMake.models : [];
    }

}
