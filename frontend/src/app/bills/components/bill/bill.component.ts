import { Component, Input, OnInit, numberAttribute } from '@angular/core';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { EntityViewComponent } from '../../../core/components/entity-view/entity-view.component';
import { UserRoles } from '../../../users/models/user.model';
import { Bill, BillCreateDto, BillUpdateDto } from '../../models/bill.model';
import { BillService } from '../../services/bill.service';

@Component({
	selector: 'se-bill',
	templateUrl: './bill.component.html',
	styleUrls: ['./bill.component.scss'],
})
export class BillComponent extends EntityViewComponent<Bill, BillCreateDto, BillUpdateDto> {
	UserRoles = UserRoles;

	constructor(
		public override authentication: AuthenticationService,
		protected override entityService: BillService,
	) {
		super(authentication, entityService)
	}

	protected override onUpdate(): void { }
}