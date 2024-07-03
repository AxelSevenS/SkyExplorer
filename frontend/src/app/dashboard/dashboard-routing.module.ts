import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoursesPage } from './courses/courses.page';
import { DashboardComponent as DashboardComponent } from './dashboard-component';
import { BillingsPage } from './billings/billings.page';
import { PlanesPage } from './planes/planes.page';
import { FlightsPage } from './flights/flights.page';

const routes: Routes = [
	{
		path: '',
		component: DashboardComponent,
		children: [
			{
				path: 'courses',
				component: CoursesPage
			},
			{
				path: 'billings',
				component: BillingsPage
			},
			{
				path: 'planes',
				component: PlanesPage
			},
			{
				path: 'flights',
				component: FlightsPage
			},
			{ path: '', redirectTo: 'courses', pathMatch: 'full' },
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class DashboardRoutingModule { }