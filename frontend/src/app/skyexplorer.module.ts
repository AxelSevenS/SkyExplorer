import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';

import { SkyExplorerRoutingModule } from './skyexplorer-routing.module';
import { AuthenticationModule } from './authentication/authentication.module';

import { SkyExplorerComponent } from './skyexplorer.component';
import { SidenavComponent } from './core/components/sidenav/sidenav-component';
import { NotFoundPage } from './core/pages/not-found-page/not-found.page';

import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

@NgModule({
	imports: [
		CommonModule,
		BrowserModule,
		HttpClientModule,
		RouterModule,
		FormsModule,
		ReactiveFormsModule,

		MatCardModule,
		MatGridListModule,
		MatFormFieldModule,
		MatButtonModule,
		MatInputModule,
		MatIconModule,
		MatDividerModule,
		MatSidenavModule,
		MatListModule,

		SkyExplorerRoutingModule,
		AuthenticationModule,
	],
	declarations: [
		SkyExplorerComponent,
		SidenavComponent,
		NotFoundPage,
	],
	providers: [
		Storage,
		provideAnimationsAsync(),
	],
	bootstrap: [SkyExplorerComponent]
})
export class SkyExplorerModule {}
