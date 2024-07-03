import { Component } from '@angular/core';
import { AuthenticationService } from './authentication/authentication.service';

@Component({
	selector: 'se-root',
	templateUrl: 'skyexplorer.component.html',
	styleUrls: ['skyexplorer.component.scss'],
})
export class SkyExplorerComponent {
	public get authentication(): AuthenticationService { return this._authentication; }

	public static readonly PAGES = [
		{
			path: 'search',
			display: 'Rechercher',
			icon: 'search-outline'
		},
		{
			path: 'create',
			display: 'Créer',
			icon: 'pencil-outline'
		}
	];

	public get pages() { return SkyExplorerComponent.PAGES; }

	constructor(
		private _authentication: AuthenticationService
	) {}
}
