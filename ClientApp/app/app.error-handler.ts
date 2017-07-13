import { ToastyService } from "ng2-toasty";
import { ErrorHandler, Inject, NgZone } from '@angular/core';


//Global Error Handling
export class AppErrorHandler implements ErrorHandler {
    constructor(private ngZone:NgZone,@Inject(ToastyService) private toastyService: ToastyService) {
        
    }
    handleError(error): void {
        this.ngZone.run(() => {
            //Error
            this.toastyService.error({
                title: 'Error',
                msg: 'An Unexpected Error Happend!',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            });

        });
        
    }
}