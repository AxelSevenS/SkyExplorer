import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

import { NotFoundPage } from './core/pages/not-found-page/not-found.page';
import { SidenavComponent } from './core/components/sidenav/sidenav-component';
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
				path: 'dashboard',
				loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
			},
			{
				path: 'courses',
				loadChildren: () => import('./courses/courses.module').then(m => m.CoursesModule),
			},
			{
				path: 'bills',
				loadChildren: () => import('./bills/bills.module').then(m => m.BillsModule),
			},
			{
				path: 'planes',
				loadChildren: () => import('./planes/planes.module').then(m => m.PlanesModule),
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
