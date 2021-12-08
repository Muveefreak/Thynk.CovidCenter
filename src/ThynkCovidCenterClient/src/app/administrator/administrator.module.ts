import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { CreateUserComponent } from './create-user/create-user.component';
import { CreateLocationComponent } from './create-location/create-location.component';
import { AvailableDateComponent } from './available-date/available-date.component';

import { AdministratorRoutes } from './administrator.routing';


@NgModule({
	imports: [FormsModule, CommonModule, RouterModule.forChild(AdministratorRoutes), ReactiveFormsModule,
		NgbModule],
	declarations: [CreateUserComponent, CreateLocationComponent, AvailableDateComponent]
})
export class AdministratorModule {}
