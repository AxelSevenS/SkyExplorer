import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserListComponent } from './user-list/user-list.component';
import { UserPage } from './user-page/user.page';

const routes: Routes = [
	{
		path: 'list',
		component: UserListComponent,
	},
	{
		path: ':id',
		component: UserPage
	},
	{ path: '', redirectTo: 'list', pathMatch: 'full' },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class UserRoutingModule {}
