import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'se-account-menu',
  templateUrl: './account-menu.component.html',
  styleUrl: './account-menu.component.scss'
})
export class AccountMenuComponent {
	public get authentication() { return this._authentication }

	constructor(
		private _authentication: AuthenticationService
	) {}
}
