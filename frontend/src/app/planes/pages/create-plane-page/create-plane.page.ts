import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { UserRoles } from '../../../users/models/user.model';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { PlaneStatus } from '../../../planes/models/plane.model';
import { PlaneService } from '../../services/plane.service';
import { PlaneCreateDto } from '../../models/plane.model';

@Component({
  selector: 'se-create-plane-page',
  templateUrl: './create-plane.page.html',
  styleUrl: './create-plane.page.scss'
})
export class CreatePlanePage implements OnInit {
	UserRoles = UserRoles;

	publishForm: FormGroup = this.formBuilder.group(
		{
			name: [''],
			type: [''],
			status: [''],
		}, {}
	);

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,

		public authentication: AuthenticationService,
		private planeService: PlaneService,
	) {
	}

	ngOnInit(): void {
		if (! this.authentication.user || this.authentication.user.role < UserRoles.Collaborator) {
			this.router.navigateByUrl(document.referrer);
			return;
		}

	}

	status: number = 0;


	public publish(): void {
		if (this.publishForm.invalid) {
			console.log(this.publishForm);
			return;
		}


		const name: string = this.publishForm.controls["name"].value;
		const type: string = this.publishForm.controls["type"].value;
		const statusIndex: number = this.publishForm.controls["status"].value;
		const status: PlaneStatus = statusIndex;

		const planeRequest = new PlaneCreateDto(
			name,
			type,
			status,
		);


		this.planeService.create(planeRequest)
			.subscribe(plane => {
				if (plane instanceof HttpErrorResponse) return;

				console.log(plane);
				this.router.navigate([`/planes/${plane.id}`]);

			});
		}
}
