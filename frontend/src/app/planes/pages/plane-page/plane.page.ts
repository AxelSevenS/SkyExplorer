import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Plane, PlaneUpdateDto,PlaneStatus} from '../../models/plane.model';
import { PlaneService } from '../../services/plane.service';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { User, UserRoles } from '../../../users/models/user.model';
import { UserService } from '../../../users/services/user.service';
import moment from 'moment';


@Component({
	selector: 'se-plane-page',
	templateUrl: 'plane.page.html',
	styleUrls: ['plane.page.scss'],
})
export class PlanePage {
	moment = moment;

	planeStatuses = Object.values(PlaneStatus).filter((value) => typeof value === 'number') as PlaneStatus[];


	PlaneStatus = PlaneStatus;
	UserRoles = UserRoles;

	status: PlaneStatus = PlaneStatus.Available;


	public get requestId(): number { return this.activatedRoute.snapshot.params['id'] }

	public get plane() { return this._plane }
	private _plane?: Plane | null;


	editPlaneForm: FormGroup = this.formBuilder.group(
		{
			status: [''],
		}
	);


	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private activatedRoute: ActivatedRoute,

		public authentication: AuthenticationService,
		private planeService: PlaneService,
	) {
	}

	ngOnInit(): void {
		this.updatePlane();

		this.planeService.eventRemoved
			.subscribe(plane => {
				if (this._plane?.id != plane.id) return;
				this._plane = null;
			});

		this.planeService.eventUpdated
			.subscribe(plane => {
				if (this._plane?.id != plane.id) return;
				this._plane = plane;
			});

		this.router.events
			.subscribe(e => {
				this.updatePlane();
			});
	}

	private updatePlane(): void {
		if (this.plane?.id === this.requestId) return;

		this.planeService.getById(this.requestId)
			.subscribe(plane => {
				if (plane instanceof HttpErrorResponse) return;

				this._plane = plane;
				this.editPlaneForm.controls['status'].setValue(PlaneStatus[plane.status]);
			});
	}


	onSubmit(): void {
		if ( ! this._plane ) return;
		if ( ! this.editPlaneForm.valid ) return;

		const updated = new PlaneUpdateDto();

		const statusInput: number = this.editPlaneForm.controls['status'].value;
		if (statusInput != this._plane.status) {
			updated.status = statusInput;
		}

		if (updated.status !== undefined) {
			this.planeService.updateById(this._plane.id, updated)
				.subscribe(async plane => {
					if (plane instanceof HttpErrorResponse || ! this.plane) return;
					this._plane = plane;
				})
		}

	}


	async delete() {
		if(! this.plane) return;

		this.planeService.deleteById(this.plane.id)
			.subscribe(async plane => {
				if (plane instanceof HttpErrorResponse || ! this.plane) return;
				this._plane = plane;
			});
	}

}
