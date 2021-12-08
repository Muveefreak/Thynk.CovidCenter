import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { FullComponent } from './layouts/full/full.component';

export const Approutes: Routes = [
	{
		path: '',
		component: FullComponent,
		children: [
			{ path: '', redirectTo: '/dashboard', pathMatch: 'full' },
      		{
				path: 'administrator',
				loadChildren: () => import('./administrator/administrator.module').then(m => m.AdministratorModule)
			},
			{
				path: 'labadmin',
				loadChildren: () => import('./labadmin/labadmin.module').then(m => m.LadAdminModule)
			},
			{
				path: 'individual',
				loadChildren: () => import('./individual/individual.module').then(m => m.IndividualModule)
			},
			{
				path: 'dashboard',
				loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
			},
			{
				path: 'component',
				loadChildren: () => import('./component/component.module').then(m => m.ComponentsModule)
			}
		]
	},
	{
		path: '**',
		redirectTo: '/dashboard'
	}
];
