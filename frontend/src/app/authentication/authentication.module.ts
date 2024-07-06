import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { MatSelectModule } from '@angular/material/select';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';

import { UsersModule } from '../users/users.module';

import { LoginPage } from './pages/login-page/login-page';
import { AccountMenuComponent } from './components/account-menu/account-menu.component';



@NgModule({
  imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule,

		MatSelectModule,
		MatMenuModule,
		MatCardModule,
		MatFormFieldModule,
		MatButtonModule,
		MatInputModule,
		MatProgressSpinnerModule,
		MatIconModule,

		UsersModule,
  ],
	declarations: [
		LoginPage,
		AccountMenuComponent,
	],
	exports: [
		LoginPage,
		AccountMenuComponent,
	],
})
export class AuthenticationModule { }
