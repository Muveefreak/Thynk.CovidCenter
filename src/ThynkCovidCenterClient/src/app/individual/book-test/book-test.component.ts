import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbPanelChangeEvent } from '@ng-bootstrap/ng-bootstrap';
import { SubmitCreateBookingRequestPayload, TestType } from 'src/app/models/booking.model';
import { AvailableDateService } from 'src/app/services/availableDate.service';
import { BookingService } from 'src/app/services/booking.service';
import { LocationService } from 'src/app/services/location.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
	selector: 'app-book-test',
	templateUrl: 'book-test.component.html',
	providers: [ DatePipe ]
})
export class BookTestComponent implements OnInit {
	formGroup: FormGroup;
	applicationUserId: string;
	locationId: string;
	availableDateId: string
	dateAvailableId: string;
	availableSlots: Number;
	testType : TestType;
	locations: [];
	myDatePickerOptions: any;
	isSubmitted = false;
	dateAvail: Date;

	constructor(private _formBuilder: FormBuilder, 
		private _availableDateService: AvailableDateService, 
		private _locationService: LocationService, 
		private _bookingService: BookingService, 
		private _notificationService:NotificationService,
		private _datePipe: DatePipe) { }

	ngOnInit(): void {
		this.formGroup = this._formBuilder.group({
			applicationUserId: new FormControl('', [Validators.required]),
			locationId: new FormControl('', [Validators.required]),
			availableDateId: new FormControl('', [Validators.required]),
			testType: new FormControl('', [Validators.required])
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

	getAvailDates() {
		// this.isAwaitingResponse = true;
		console.log(this.formGroup.controls.locationId.value);
		this._availableDateService.GetAvailDates(this.formGroup.controls.locationId.value).subscribe(
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

	createBooking() {
		console.log("HERE Booking");
		this.isSubmitted = true;
		if (this.formGroup.valid) {
		const requestPayload = this.buildSubmitRequestPayload();
		console.log(requestPayload);
		// this.isAwaitingResponse = true;
		this._bookingService.CreateBooking(requestPayload).subscribe(
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

	buildSubmitRequestPayload(): SubmitCreateBookingRequestPayload {
		return {
			applicationUserId: this.formGroup.controls.applicationUserId.value,
			locationID: this.locationId,
			availableDateId: this.formGroup.controls.availableDateId.value,
			testType: this.formGroup.controls.testType.value
		};
	}
	selectLocationId(e: any)
	{
		this.locationId = this.formGroup.get('locationId')?.value;
		console.log(this.locationId);
		//this.getAvailDates();
		
	}

	selectDateAvailableId(e: any)
	{
		this.dateAvailableId = this.formGroup.get('dateAvailableId')?.value;
		this.availableSlots = 2;
	}

	resetForm() {
		this.formGroup.reset();
	}

	onDateChange(newDate: Date) {
		console.log(newDate);
	}
}
