import { Component, Input, OnInit, numberAttribute } from '@angular/core';
import { PlaneService } from '../../services/plane.service';
import { Plane, PlaneCreateDto, PlaneUpdateDto } from '../../models/plane.model';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { EntityViewComponent } from '../../../core/components/entity-view/entity-view.component';
import { UserRoles } from '../../../users/models/user.model';
import moment from 'moment';

@Component({
	selector: 'se-plane',
	templateUrl: './plane.component.html',
	styleUrls: ['./plane.component.scss'],
})
export class PlaneComponent extends EntityViewComponent<Plane, PlaneCreateDto, PlaneUpdateDto> {
	UserRoles = UserRoles;

	moment = moment;

	constructor(
		public override authentication: AuthenticationService,
		protected override entityService: PlaneService,
	) {
		super(authentication, entityService)
	}
}