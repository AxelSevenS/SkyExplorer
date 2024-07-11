import { Component } from '@angular/core';
import { BillService } from '../../services/bill.service';
import { Bill } from '../../models/bill.model';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { UserService } from '../../../users/services/user.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
	selector: 'se-bill-list-page',
	templateUrl: 'bill-list.page.html',
	styleUrls: ['bill-list.page.scss'],
})
export class BillListPage {
	public bills: Bill[] = [];
	constructor(
		public billService: BillService,
		public authentication: AuthenticationService,
		public userService: UserService,
	) {

	}
	ngOnInit() {
		this.billService.getAll()
			.subscribe(res => {
				if (res instanceof HttpErrorResponse) return;
				this.bills = res;
			});
	}
}
