import * as $ from 'jquery';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
	CommonModule,
	LocationStrategy
} from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';

import { NgbModule, NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
// import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';


import { FullComponent } from './layouts/full/full.component';

import { NavigationComponent } from './shared/header-navigation/navigation.component';
import { SidebarComponent } from './shared/sidebar/sidebar.component';
import { BreadcrumbComponent } from './shared/breadcrumb/breadcrumb.component';

import { Approutes } from './app-routing.module';
import { AppComponent } from './app.component';
import { SpinnerComponent } from './shared/spinner.component';


@NgModule({
	declarations: [
		AppComponent,
		SpinnerComponent,
		FullComponent,
		NavigationComponent,
		BreadcrumbComponent,
		SidebarComponent
	],
	imports: [
		CommonModule,
		BrowserModule,
		BrowserAnimationsModule,
		FormsModule,
		ReactiveFormsModule,
		HttpClientModule,
		NgbModule,
		NgbDatepickerModule,
		BsDatepickerModule,
		ToastrModule.forRoot(),
		// BsDatepickerModule.forRoot(),
		RouterModule.forRoot(Approutes, { useHash: false, relativeLinkResolution: 'legacy' })
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
