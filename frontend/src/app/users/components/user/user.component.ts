import { Component, Input, OnInit, numberAttribute } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User, UserCreateDto, UserUpdateDto } from '../../models/user.model';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { EntityViewComponent } from '../../../core/components/entity-view/entity-view.component';

@Component({
	selector: 'se-user',
	templateUrl: './user.component.html',
	styleUrls: ['./user.component.scss'],
})
export class UserComponent extends EntityViewComponent<User, UserCreateDto, UserUpdateDto> {
	constructor(
		public override authentication: AuthenticationService,
		protected override entityService: UserService,
	) {
		super(authentication, entityService)
	}
}