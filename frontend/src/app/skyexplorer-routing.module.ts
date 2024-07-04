import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { SkyExplorerModule } from './skyexplorer.module';
import { SkyExplorerComponent } from './skyexplorer.component';
import { NotFoundPage } from './not-found/not-found.page';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

const routes: Routes = [
	{
		path: 'auth',
		loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule),
	},
	{
		path: 'users',
		loadChildren: () => import('./user/user.module').then(m => m.UserModule),
	},
	{
		path: '',
		loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
	},
	{ path: '**', component: NotFoundPage, }
];
@NgModule({
	imports: [
		SkyExplorerModule,
		RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules, useHash: false })
	],
	// exports: [RouterModule],
	bootstrap: [SkyExplorerComponent],
	providers: [
		provideAnimationsAsync()
	],
})
export class SkyExplorerRoutingModule {}
