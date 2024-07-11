import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';

import { UsersRoutingModule } from './users-routing.module';
import { UserComponent } from './components/user/user.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserPage } from './pages/user-page/user.page';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';


@NgModule({
  imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule,

		MatProgressSpinnerModule,
		MatCardModule,
		MatFormFieldModule,
		MatInputModule,
		MatButtonModule,
		MatOptionModule,
		MatSelectModule,
		MatIconModule,
		MatProgressSpinnerModule,

		UsersRoutingModule,
  ],
  declarations: [
		UserComponent,
		UserListComponent,
		UserPage,
	],
	exports: [
		UserComponent,
		UserListComponent,
		UserPage,
	]
})
export class UsersModule { }
