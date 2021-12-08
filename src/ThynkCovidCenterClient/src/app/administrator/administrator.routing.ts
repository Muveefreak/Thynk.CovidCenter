import { Routes } from '@angular/router';

import { CreateUserComponent } from './create-user/create-user.component';
import { CreateLocationComponent } from './create-location/create-location.component';
import { AvailableDateComponent } from './available-date/available-date.component';


export const AdministratorRoutes: Routes = [
	{
		path: '',
		children: [
			{
				path: 'create-user',
				component: CreateUserComponent,
				data: {
					title: 'Create User'
				}
			},
			{
				path: 'create-location',
				component: CreateLocationComponent,
				data: {
					title: 'Create Location'
				}
			},
			{
				path: 'create-date',
				component: AvailableDateComponent,
				data: {
					title: 'Available Date'
				}
			}
		]
	}
];
