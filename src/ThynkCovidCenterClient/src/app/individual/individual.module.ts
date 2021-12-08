import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { BookTestComponent } from './book-test/book-test.component';

import { IndividualRoutes } from './individual.routing';
import { CancelBookingComponent } from './cancel-booking/cancel-booking.component';


@NgModule({
	imports: [FormsModule, CommonModule, RouterModule.forChild(IndividualRoutes), ReactiveFormsModule,
		NgbModule],
	declarations: [BookTestComponent, CancelBookingComponent]
})
export class IndividualModule {}
