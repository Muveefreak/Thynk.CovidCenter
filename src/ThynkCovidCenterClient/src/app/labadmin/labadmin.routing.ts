import { Routes } from '@angular/router';

import { ReportComponent } from './report/report.component';
import { EnterResultComponent } from './enter-result/enter-result.component';


export const LabAdminRoutes: Routes = [
	{
		path: '',
		children: [
			{
				path: 'report',
				component: ReportComponent,
				data: {
					title: 'Report'
				}
			},
			{
				path: 'enter-result',
				component: EnterResultComponent,
				data: {
					title: 'Enter Result'
				}
			}
		]
	}
];
