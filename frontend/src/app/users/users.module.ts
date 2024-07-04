import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './components/user/user.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserPage } from './pages/user-page/user.page';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { UsersRoutingModule } from './users-routing.module';



@NgModule({
  declarations: [
		UserComponent,
		UserListComponent,
		UserPage,
	],
  imports: [
		UsersRoutingModule,

		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule,

		MatFormFieldModule,
		MatOptionModule,
		MatSelectModule,
		MatCardModule,
		MatProgressSpinnerModule,
		MatIconModule,
  ],
	exports: [
		UserComponent,
		UserListComponent,
		UserPage,
	]
})
export class UsersModule { }
