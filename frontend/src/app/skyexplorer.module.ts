import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { SkyExplorerComponent } from './skyexplorer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NotFoundPage } from './not-found/not-found.page';

@NgModule({
	imports: [
		BrowserModule,
		HttpClientModule,
		FormsModule,
		RouterModule,
		ReactiveFormsModule,
	],
	declarations: [
		SkyExplorerComponent,
		NotFoundPage
	],
	providers: [
		Storage
	],
})
export class SkyExplorerModule {}
