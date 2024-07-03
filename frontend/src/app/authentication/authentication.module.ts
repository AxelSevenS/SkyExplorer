import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthPage } from './auth-page/auth-page';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { AuthenticationRoutingModule } from './authentication-routing.module';

@NgModule({
	imports: [
		CommonModule,
		HttpClientModule,
		FormsModule,
		ReactiveFormsModule,
		MatCardModule,
		MatGridListModule,
		MatFormFieldModule,
		MatButtonModule,
		MatInputModule,
		MatIconModule,
		MatDividerModule,

		AuthenticationRoutingModule,
	],
	declarations: [AuthPage],
	exports: [AuthPage]
})
export class AuthenticationModule {
}
