import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User, UserRoles, UserUpdateDto } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { first } from 'rxjs';
import { AuthenticationValidators } from '../../../authentication/validators/authentication-validators';

@Component({
	selector: 'se-user-page',
	templateUrl: 'user.page.html',
	styleUrls: ['user.page.scss'],
})
export class UserPage {

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
		if (this._authentication.user == null) return [];

		return this.userService.getSubserviantRoles(this._authentication.user.role);
	}

	public get authentication(): AuthenticationService { return this._authentication }
	public get requestId(): number { return this.activatedRoute.snapshot.params['id'] }

	public get user() { return this._user }
	private _user?: User | null;



	constructor(
		private router: Router,
		private formBuilder: FormBuilder,
		private activatedRoute: ActivatedRoute,
		private userService: UserService,
		private _authentication: AuthenticationService,
	) {}

	ngOnInit(): void {
		this.updateUser();

		this.userService.eventRemoved
			.subscribe(user => {
				if (this._user?.id != user.id) return;
				this._user = null;
			});

		this.userService.eventUpdated
			.subscribe(user => {
				if (this._user?.id != user.id) return;
				this._user = user;
			});

		this.router.events
			.subscribe(e => {
				this.updateUser();
			});
	}

	private updateUser(): void {
		if (this.user?.id === this.requestId) return;

		this.userService.getById(this.requestId)
			.subscribe(user => {
				if (user instanceof HttpErrorResponse) return;

				this._user = user;

				this.editUserForm.controls['email'].setValue(user.email);
				this.editUserForm.controls['role'].setValue(UserRoles[user.role]);
			});
	}

	onSubmit(): void {
		if ( ! this.user ) return;
		if ( ! this.editUserForm.valid ) return;

		let updated = new UserUpdateDto();

		let emailInput = this.editUserForm.controls['email'];
		if (emailInput.value !== this.user.email) {
			updated.email = emailInput.value;
		}

		let roleInput = this.editUserForm.controls['role'];
		if (roleInput.value !== this.user.role) {
			updated.role = roleInput.value;
		}

		let passwordInput = this.editUserForm.controls['password'];
		if (! passwordInput.value) {
			updated.password = passwordInput.value;
		}

		if (! updated.email && ! updated.password && ! updated.role) return;

		this.userService.updateById(this.requestId, updated)
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

	async delete() {
		if(! this.user) return;

		this.userService.deleteById(this.user.id)
			.subscribe(async res => {
				if (res instanceof HttpErrorResponse) {
					// const alert = await this.alertController.create({
					// 	header: 'Erreur lors de la Suppression de l\'Utilisateur',
					// 	message: 'La suppression de l\'Utilisateur a échoué',
					// 	buttons: ['Ok'],
					// });

					// await alert.present();
					return;
				}
			});
	}

}
