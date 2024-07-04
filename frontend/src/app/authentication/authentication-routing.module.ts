import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthPage } from './auth-page/auth-page';
import { PasswordEditPage } from './password-edit-page/password-edit.page';

const routes: Routes = [
	{
		path: 'login',
		component: AuthPage
	},
	{
		path: 'edit-password',
		component: PasswordEditPage
	},
	{ path: '', redirectTo: 'login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class AuthenticationRoutingModule {}
