import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { UserRoles } from '../../models/user.model';

@Component({
	selector: 'se-user-list',
	templateUrl: 'user-list.component.html',
	styleUrls: ['user-list.component.scss'],
})
export class UserListComponent implements OnInit {
	UserRoles = UserRoles;

	public get users() { return this._users }
	private _users?: User[];

	constructor(
		private userService: UserService,
		public authentication: AuthenticationService,
	) { }

	ngOnInit(): void {
		let usersObservable = this.authentication.user && this.authentication.user.role >= UserRoles.Collaborator
			? this.userService.getAll()
			: this.userService.getByRole(UserRoles.Collaborator);

		usersObservable
			.subscribe(users => {
				this._users = [];
				if (users instanceof HttpErrorResponse) return;

				this._users = users;
			});
	}

	onInfiniteScroll(event: Event) {
	}

}
