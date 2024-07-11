import { Component } from '@angular/core';
import { PlaneService } from '../../services/plane.service';
import { Plane } from '../../models/plane.model';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { UserService } from '../../../users/services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { UserRoles } from '../../../users/models/user.model';

@Component({
	selector: 'se-plane-list-page',
	templateUrl: 'plane-list.page.html',
	styleUrls: ['plane-list.page.scss'],
})
export class PlaneListPage {
	UserRoles = UserRoles;

	public planes?: Plane[];

	constructor(
		public authentication: AuthenticationService,
		public planeService: PlaneService,
	) {

	}
	ngOnInit() {
		this.planeService.getAll()
			.subscribe(res => {
				if (res instanceof HttpErrorResponse) return;
				this.planes = res;
			});
	}
}
