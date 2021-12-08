import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SubmitCreateLocationRequestPayload } from 'src/app/models/location.models';
import { LocationService } from 'src/app/services/location.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
	selector: 'app-create-location',
	templateUrl: 'create-location.component.html'
})
export class CreateLocationComponent implements OnInit {
	formGroup: FormGroup;

	constructor(private _formBuilder: FormBuilder, private _locationService: LocationService, private _notificationService:NotificationService) { }

	ngOnInit(): void {
		this.formGroup = this._formBuilder.group({
			name: new FormControl('', [Validators.required]),
			applicationUserId: new FormControl('', [Validators.required]),
			description: new FormControl('', []),
		});
	}

	createLocation() {
		if (this.formGroup.valid) {
		const requestPayload = this.buildSubmitRequestPayload();
		console.log(requestPayload);
        // this.isAwaitingResponse = true;
		this._locationService.CreateLocation(requestPayload).subscribe(
			(response: any) => {
				console.log(response);
				this.formGroup.reset();
				if (response && response.status) {
					console.log(response);
					this._notificationService.showSuccess(response.message, "Success");
					// this.isAwaitingResponse = false;
				}
				else {
					console.log(response);
					this._notificationService.showError(response.message, "Error");
					// this.isAwaitingResponse = false;
				}
			},
			(err : any) => {
				this._notificationService.showError("We encountered a technical error while processing request. Please try again after some time","Error");
				// this.isAwaitingResponse = false;
			});
		}
	}

	buildSubmitRequestPayload(): SubmitCreateLocationRequestPayload {
  
		return {
		  	name: this.formGroup.controls.name.value,
			description: this.formGroup.controls.description.value,
			applicationUserId: this.formGroup.controls.applicationUserId.value,
		};
	  }

	resetForm() {
		this.formGroup.reset();
	}
}
