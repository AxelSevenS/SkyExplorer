import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Plane, PlaneUpdateDto,PlaneStatus, PlaneCreateDto} from '../../models/plane.model';
import { PlaneService } from '../../services/plane.service';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { UserRoles } from '../../../users/models/user.model';
import moment from 'moment';
import { EntityViewComponent } from '../../../core/components/entity-view/entity-view.component';


@Component({
	selector: 'se-plane-page',
	templateUrl: 'plane.page.html',
	styleUrls: ['plane.page.scss'],
})
export class PlanePage extends EntityViewComponent<Plane, PlaneCreateDto, PlaneUpdateDto> {
	moment = moment;

	planeStatuses = Object.values(PlaneStatus).filter((value) => typeof value === 'number') as PlaneStatus[];


	PlaneStatus = PlaneStatus;
	UserRoles = UserRoles;

	status: PlaneStatus = PlaneStatus.Available;


	editPlaneForm: FormGroup = this.formBuilder.group(
		{
			status: [''],
		}
	);


	constructor(
		private formBuilder: FormBuilder,
		private router: Router,

		public override authentication: AuthenticationService,
		private planeService: PlaneService,
		private activatedRoute: ActivatedRoute,
	) {
		super(authentication, planeService);
	}

	override ngOnInit(): void {
		super.ngOnInit();

		this.router.events
			.subscribe(e => {
				this.id = this.activatedRoute.snapshot.params['id'];
			});
	}

	protected override onUpdate(): void {
		this.editPlaneForm.controls['status'].setValue(this.entity?.status);
	}


	onSubmit(): void {
		if ( ! this.entity ) return;
		if ( ! this.editPlaneForm.valid ) return;

		const updated = new PlaneUpdateDto();

		const statusInput: number = this.editPlaneForm.controls['status'].value;
		if (statusInput != this.entity.status) {
			updated.status = statusInput;
		}

		if (updated.status !== undefined) {
			this.planeService.updateById(this.entity.id, updated)
				.subscribe(async plane => {
					if (plane instanceof HttpErrorResponse || ! this.entity) return;
					this.entity = plane;
				})
		}
	}
}
