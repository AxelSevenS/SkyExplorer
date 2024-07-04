import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';

import { UserListComponent } from './user-list/user-list.component';
import { UserPage } from './user-page/user.page';
import { UserComponent } from './user/user.component';
import { PasswordEditPage } from '../authentication/password-edit-page/password-edit.page';
import { UserRoutingModule } from './user-routing.module';

@NgModule({
	imports: [
		UserRoutingModule,
		CommonModule,
		FormsModule,
		MatSelectModule,
		MatFormFieldModule,
		ReactiveFormsModule,
	],
	declarations: [
		UserListComponent,
		UserComponent,
		UserPage,
		PasswordEditPage,
	],
	exports: [
		UserListComponent,
		UserComponent,
	],
})
export class UserModule { }