import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User, UserCreateDto, UserRoles, UserUpdateDto } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { first } from 'rxjs';
import { AuthenticationValidators } from '../../../authentication/validators/authentication-validators';
import { EntityViewComponent } from '../../../core/components/entity-view/entity-view.component';

@Component({
	selector: 'se-user-page',
	templateUrl: 'user.page.html',
	styleUrls: ['user.page.scss'],
})
export class UserPage extends EntityViewComponent<User, UserCreateDto, UserUpdateDto> {
	UserRoles = UserRoles;

	editUserForm: FormGroup = this.formBuilder.group(
		{
			email: [''],
			role: [''],
			password: ['', AuthenticationValidators.securePasswordValidator],
			passwordConfirm: ['', AuthenticationValidators.securePasswordValidator],
		},
		{
			validators: AuthenticationValidators.confirmPasswordValidator('password', 'passwordConfirm')
		}
	);

	public get subRoles(): UserRoles[] {
		if (this.authentication.user == null) return [];

		return this.userService.getSubserviantRoles(this.authentication.user.role);
	}



	constructor(
		private router: Router,
		private activatedRoute: ActivatedRoute,
		public override authentication: AuthenticationService,
		private formBuilder: FormBuilder,
		private userService: UserService,
	) {
		super(authentication, userService)
	}

	override ngOnInit(): void {
		super.ngOnInit();

		this.router.events
			.subscribe(e => {
				this.id = this.activatedRoute.snapshot.params['id'];
			});
	}

	protected override onUpdate(): void {
		this.editUserForm.controls['email'].setValue(this.entity?.email);
		this.editUserForm.controls['role'].setValue(this.entity?.role);
	}

	onSubmit(): void {
		if ( ! this.entity ) return;
		if ( ! this.editUserForm.valid ) return;

		const updated = new UserUpdateDto();

		const emailInput: string = this.editUserForm.controls['email'].value;
		if (emailInput !== this.entity.email) {
			updated.email = emailInput;
		}

		const passwordInput: string = this.editUserForm.controls['password'].value;
		if (! passwordInput) {
			updated.password = passwordInput;
		}

		const roleInput: number = this.editUserForm.controls['role'].value;
		if (roleInput != this.entity.role) {
			updated.role = roleInput;
		}

		if (updated.email !== undefined || updated.password !== undefined || updated.role !== undefined) {
			this.userService.updateById(this.entity.id, updated)
				.subscribe(async res => {
					if (res instanceof HttpErrorResponse) {
						// const alert = await this.alertController.create({
						// 	header: 'Erreur lors de la Modification de l\'Utilisateur',
						// 	message: `La modification de l\'Utilisateur à échoué (erreur ${res.statusText})`,
						// 	buttons: ['Ok'],
						// });

						// await alert.present();
						return;
					}
				});
		}

	}
}
