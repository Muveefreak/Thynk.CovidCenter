import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbPanelChangeEvent } from '@ng-bootstrap/ng-bootstrap';
import { BookingResultType, SubmitEnterResultRequestPayload } from 'src/app/models/report.model';
import { NotificationService } from 'src/app/services/notification.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
	selector: 'app-enter-result',
	templateUrl: 'enter-result.component.html'
})
export class EnterResultComponent implements OnInit {
	formGroup: FormGroup;
	applicationUserId: string;
	locationId: string;
	availableDateId: string
	dateAvailableId: string;
	availableSlots: Number;
	resultType: BookingResultType;
	locations: [];
	myDatePickerOptions: any;
	isSubmitted = false;
	dateAvail: Date;

	constructor(private _formBuilder: FormBuilder,
		private _reportService: ReportService,
		private _notificationService: NotificationService) { }

	ngOnInit(): void {
		this.formGroup = this._formBuilder.group({
			applicationUserId: new FormControl('', [Validators.required]),
			id: new FormControl('', [Validators.required]),
			bookingResult: new FormControl('', []),
		});
	}

	enterResultBooking() {
		this.isSubmitted = true;
		if (this.formGroup.valid) {
			const requestPayload = this.buildSubmitRequestPayload();
			console.log(requestPayload);
			// this.isAwaitingResponse = true;
			this._reportService.EnterResultBooking(requestPayload).subscribe(
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
				(err: any) => {
					this._notificationService.showError("We encountered a technical error while processing request. Please try again after some time", "Error");
					// this.isAwaitingResponse = false;
				});
		}
	}

	resetForm() {
		this.formGroup.reset();
	}

	buildSubmitRequestPayload(): SubmitEnterResultRequestPayload {
		return {
			applicationUserId: this.formGroup.controls.applicationUserId.value,
			bookingResult: this.formGroup.controls.bookingResult.value,
			id: this.formGroup.controls.id.value
		};
	}
}
