import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginPage } from './pages/login-page/login-page';
import { PasswordEditPage } from './pages/password-edit-page/password-edit.page';
import { UsersModule } from '../users/users.module';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';



@NgModule({
	declarations: [
		LoginPage,
		PasswordEditPage,
	],
  imports: [
		CommonModule,
		FormsModule,

		MatCardModule,
		MatSelectModule,
		MatFormFieldModule,
		MatButtonModule,
		MatInputModule,
		ReactiveFormsModule,
		MatProgressSpinnerModule,
		MatIconModule,

		UsersModule,
  ],
	exports: [
		LoginPage,
		PasswordEditPage,
	],
})
export class AuthenticationModule { }
