import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { MatCardModule, MatCardTitle } from '@angular/material/card';
import { DashboardPage } from './pages/dashboard.page';
import { CoursesModule } from '../courses/courses.module';


@NgModule({
	imports: [
		DashboardRoutingModule,

    CommonModule,

		MatCardModule,
		MatCardTitle,

		CoursesModule,
  ],
	exports: [
		DashboardPage
	],
	declarations: [
		DashboardPage
	],
})
export class DashboardModule { }
