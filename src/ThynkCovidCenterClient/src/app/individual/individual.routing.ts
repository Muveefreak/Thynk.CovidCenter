import { Routes } from '@angular/router';

import { BookTestComponent } from './book-test/book-test.component';
import { CancelBookingComponent } from './cancel-booking/cancel-booking.component';


export const IndividualRoutes: Routes = [
	{
		path: '',
		children: [
			{
				path: 'book-test',
				component: BookTestComponent,
				data: {
					title: 'Book Test'
				}
			},
			{
				path: 'cancel-booking',
				component: CancelBookingComponent,
				data: {
					title: 'Cancel Booking'
				}
			}
		]
	}
];
