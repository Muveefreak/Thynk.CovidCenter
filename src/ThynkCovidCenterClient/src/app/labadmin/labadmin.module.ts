import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { ReportComponent } from './report/report.component';
import { EnterResultComponent } from './enter-result/enter-result.component';

import { LabAdminRoutes } from './labadmin.routing';


@NgModule({
	imports: [FormsModule, CommonModule, RouterModule.forChild(LabAdminRoutes), ReactiveFormsModule,
		NgbModule],
	declarations: [ReportComponent, EnterResultComponent]
})
export class LadAdminModule {}
