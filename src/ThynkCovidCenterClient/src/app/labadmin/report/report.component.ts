import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbPanelChangeEvent } from '@ng-bootstrap/ng-bootstrap';
import { SubmitGetReportRequestPayload } from 'src/app/models/report.model';
import { NotificationService } from 'src/app/services/notification.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
	selector: 'app-report',
	templateUrl: 'report.component.html'
})
export class ReportComponent implements OnInit {
	formGroup: FormGroup;
	applicationUserId: string;
	locationId: string;
	availableDateId: string
	dateAvailableId: string;
	availableSlots: Number;
	reports: [];
	myDatePickerOptions: any;
	isSubmitted = false;
	dateAvail: Date;

	constructor(private _formBuilder: FormBuilder,
		private _reportService: ReportService,
		private _notificationService: NotificationService) { }

	ngOnInit(): void {
		this.formGroup = this._formBuilder.group({
			applicationUserId: new FormControl('', [Validators.required]),
			locationId: new FormControl('', [Validators.required]),
		});
		// this.formGroup.addControl("dateAvailable", new FormControl({day: day, month:month, year:year})) //nice
		//this.getReport();
	}

	getReport() {
		// this.isAwaitingResponse = true;
		console.log("here");
		const requestPayload = this.buildSubmitRequestPayload();
		this._reportService.GetReport(requestPayload).subscribe(
			(response: any) => {
				console.log(response);
				if (response && response.status) {
					console.log(response);
					this._notificationService.showSuccess(response.message, "Success");
					this.reports = response.data;
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
	resetForm() {
		this.formGroup.reset();
	}

	buildSubmitRequestPayload(): SubmitGetReportRequestPayload {
		return {
			applicationUserId: this.formGroup.controls.applicationUserId.value,
			locationId: this.formGroup.controls.locationId.value,
		};
	}

}
