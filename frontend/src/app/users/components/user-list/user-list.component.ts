import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { first } from 'rxjs';

@Component({
	selector: 'se-user-list',
	templateUrl: 'user-list.component.html',
	styleUrls: ['user-list.component.scss'],
})
export class UserListComponent implements OnInit {

	public get users() { return this._users }
	private _users?: User[] | null;

	constructor(
		private userService: UserService
	) { }

	ngOnInit(): void {
		this.userService.getAll()
			.subscribe(users => {
				this._users = null;
				if (users instanceof HttpErrorResponse) return;

				this._users = users;
			});
	}

	onInfiniteScroll(event: Event) {
	}

}
