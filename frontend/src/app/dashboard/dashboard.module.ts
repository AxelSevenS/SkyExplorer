import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardComponent } from './dashboard-component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterModule } from '@angular/router';
import { MatListModule } from '@angular/material/list';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { BillingsPage } from './billings/billings.page';
import { CoursesPage } from './courses/courses.page';
import { FlightsPage } from './flights/flights.page';
import { PlanesPage } from './planes/planes.page';

@NgModule({
	imports: [
		DashboardRoutingModule,
		CommonModule,
		HttpClientModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule,
		MatCardModule,
		MatGridListModule,
		MatFormFieldModule,
		MatButtonModule,
		MatInputModule,
		MatIconModule,
		MatDividerModule,
		MatSidenavModule,
		MatListModule,
	],
	declarations: [
		DashboardComponent,
		BillingsPage,
		CoursesPage,
		FlightsPage,
		PlanesPage,
	],
})
export class DashboardModule { }