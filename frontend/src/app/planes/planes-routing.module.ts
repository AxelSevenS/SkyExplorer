import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlanePage } from './pages/plane-page/plane.page';
import { CreatePlanePage } from './pages/create-plane-page/create-plane.page';
import { PlaneListPage } from './pages/plane-list-page/plane-list.page';

const routes: Routes = [
	{
		path: '',
		component: PlaneListPage
	},
	{
		path: 'create',
		component: CreatePlanePage,
	},
	{
		path: ':id',
		component: PlanePage,
	},

	{ path: '', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class PlanesRoutingModule { }
