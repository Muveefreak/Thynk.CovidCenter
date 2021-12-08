import { Component, OnInit } from '@angular/core';
import { NgbPanelChangeEvent } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SubmitCreateRequestPayload, UserRole } from 'src/app/models/user.models';
import { ResponseCodes } from 'src/app/models/enums';
import { UserService } from 'src/app/services/user.service';
import { NotificationService } from 'src/app/services/notification.service';


@Component({
	selector: 'app-create-user',
	templateUrl: 'create-user.component.html'
})
export class CreateUserComponent implements OnInit {
	formGroup: FormGroup;
	userName: string;
	email: string;
	password: string;
	disabled: boolean;
	userRole: UserRole;
	userRoles: UserRole;

	constructor(private _formBuilder: FormBuilder, private _userService: UserService, private _notificationService:NotificationService) { }

	
	ngOnInit(): void {
		this.formGroup = this._formBuilder.group({
			userName: new FormControl('', [Validators.required]),
			password: new FormControl('', [Validators.required]),
			email: new FormControl('', [Validators.required, Validators.email]),
			userRole: new FormControl('', [])
		});
	}

	createUser() {
		if (this.formGroup.valid) {
		const requestPayload = this.buildSubmitRequestPayload();
		console.log(requestPayload);
        // this.isAwaitingResponse = true;
		this._userService.CreateUser(requestPayload).subscribe(
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

	buildSubmitRequestPayload(): SubmitCreateRequestPayload {
  
		return {
		  	userName: this.formGroup.controls.userName.value,
			email: this.formGroup.controls.email.value,
			password: this.formGroup.controls.password.value,
			userRole: this.formGroup.controls.userRole.value
		};
	  }

	resetForm() {
		this.formGroup.reset();
	}
}
