import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

import { NotFoundPage } from './core/pages/not-found-page/not-found.page';
import { SidenavComponent } from './core/components/sidenav/sidenav-component';
import { BillsPage } from './bills/pages/bills-page/bills.page';
import { PlanesPage } from './planes/pages/planes-page/planes.page';
import { FlightsPage } from './flights/pages/flights-page/flights.page';
import { LoginPage } from './authentication/pages/login-page/login-page';
import { DashboardPage } from './core/pages/dashboard-page/dashboard.page';

const routes: Routes = [
	{
		path: 'login',
		component: LoginPage
	},
	{
		path: '',
		component: SidenavComponent,
		children: [
			{
				path: 'dashboard',
				loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
			},
			{
				path: 'courses',
				loadChildren: () => import('./courses/courses.module').then(m => m.CoursesModule),
			},
			{
				path: 'bills',
				component: BillsPage
			},
			{
				path: 'planes',
				component: PlanesPage
			},
			{
				path: 'flights',
				component: FlightsPage
			},

			{
				path: 'users',
				loadChildren: () => import('./users/users.module').then(m => m.UsersModule),
			},

			{ path: '', redirectTo: 'courses', pathMatch: 'full' },
		]
	},

	{ path: '**', component: NotFoundPage, }
];

@NgModule({
	imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules, useHash: false })],
	exports: [RouterModule],
})
export class SkyExplorerRoutingModule {}
