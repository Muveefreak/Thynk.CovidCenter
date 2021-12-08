import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbPanelChangeEvent } from '@ng-bootstrap/ng-bootstrap';
import { SubmitAvailDateRequestPayload } from 'src/app/models/availDate.model';
import { AvailableDateService } from 'src/app/services/availableDate.service';
import { NotificationService } from 'src/app/services/notification.service';
import { DatePipe, formatDate } from '@angular/common';
import { LocationService } from 'src/app/services/location.service';

@Component({
	selector: 'app-available-date',
	templateUrl: 'available-date.component.html',
	providers: [ DatePipe ]
})
export class AvailableDateComponent implements OnInit {
	formGroup: FormGroup;
	applicationUserId: string;
	locationId: string;
	dateAvailable: Date;
	availableSlots: Number;
	locations: [];
	myDatePickerOptions: any;
	isSubmitted = false;
	dateAvail: Date;

	constructor(private _formBuilder: FormBuilder, 
		private _locationService: LocationService, 
		private _notificationService:NotificationService,
		private _availableDateService:AvailableDateService,
		private _datePipe: DatePipe) { }

	ngOnInit(): void {
		this.formGroup = this._formBuilder.group({
			applicationUserId: new FormControl('', [Validators.required]),
			locationId: new FormControl('', [Validators.required]),
			availableSlots: new FormControl('', [Validators.required]),
			dateAvailable: new FormControl('', [])
		});
		// this.formGroup.addControl("dateAvailable", new FormControl({day: day, month:month, year:year})) //nice
		this.getLocations();
	}

	getLocations() {
    // this.isAwaitingResponse = true;
	this._locationService.GetLocations().subscribe(
		(response: any) => {
			console.log(response);
			if (response && response.status) {
				console.log(response);
				this._notificationService.showSuccess(response.message, "Success");
				this.locations = response.data;
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

	createAvailableDate() {
		this.isSubmitted = true;
		if (this.formGroup.valid) {
		const requestPayload = this.buildSubmitRequestPayload();
		console.log(requestPayload);
        // this.isAwaitingResponse = true;
		this._availableDateService.CreateAvailDates(requestPayload).subscribe(
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

	buildSubmitRequestPayload(): SubmitAvailDateRequestPayload {
		console.log("====> ", this.formGroup.controls.dateAvailable);
		console.log("===>", this.dateAvail);
		
		console.log("====>", this.formGroup.get('dateAvailable'));
		
		return {
			applicationUserId: this.formGroup.controls.applicationUserId.value,
			locationId: this.locationId,
			// dateAvailable: formatDate(this.formGroup.controls.dateAvailable.value, 'yyyy-MM-dd', 'en'),
			dateAvailable: this._datePipe.transform(this.formGroup.controls.dateAvailable.value, 'yyyy-MM-dd'),
			availableSlots: this.formGroup.controls.availableSlots.value
		};
	  }
	
	selectLocationId(e: any)
	{
		this.locationId = this.formGroup.get('locationId')?.value;
	}

	resetForm() {
		this.formGroup.reset();
	}

	onDateChange(newDate: Date) {
		console.log(newDate);
	  }

	// onDateChanged(e: any){
	// 	this.dateAvailable = this.formGroup.get('dateAvailable')?.value;
	// }
	disabled = false;
}
