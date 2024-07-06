import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

import { NotFoundPage } from './core/pages/not-found-page/not-found.page';
import { SidenavComponent } from './core/components/sidenav/sidenav-component';
import { CoursesPage } from './courses/pages/courses-page/courses.page';
import { BillingsPage } from './billings/pages/billings-page/billings.page';
import { PlanesPage } from './planes/pages/planes-page/planes.page';
import { FlightsPage } from './flights/pages/flights-page/flights.page';
import { LoginPage } from './authentication/pages/login-page/login-page';

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
