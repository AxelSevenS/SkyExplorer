import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../user/user.model';
import { UserService } from '../../user/user.service';
import { AuthenticationService } from '../authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationValidators } from '../authentication-utility';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
	selector: 'se-password-edit',
	templateUrl: 'password-edit.page.html',
	styleUrls: ['password-edit.page.scss'],
})
export class PasswordEditPage {

	editUserForm: FormGroup = this.formBuilder.group(
		{
			password: ['', Validators.compose([Validators.required, AuthenticationValidators.securePasswordValidator])],
			passwordConfirm: ['', Validators.compose([Validators.required, AuthenticationValidators.securePasswordValidator])],
		},
		{
			validators: AuthenticationValidators.confirmPasswordValidator('password', 'passwordConfirm')
		}
	);

	public get authentication(): AuthenticationService { return this._authentication }
	public get requestId(): number { return this.activatedRoute.snapshot.params['id'] }

	public get user() { return this._user }
	private _user?: User | null;



	constructor(
		private formBuilder: FormBuilder,
		private activatedRoute: ActivatedRoute,
		private userService: UserService,
		private _authentication: AuthenticationService
	) {}

	ngOnInit(): void {
	}

	onSubmit(): void {
		if ( ! this.authentication.user ) return;
		if ( ! this.editUserForm.valid ) return;

		let password: string = this.editUserForm.controls['password'].value;

		this.userService.updateUserById(this.authentication.user.id, undefined, password)
			.subscribe( async res => {
				if (res instanceof HttpErrorResponse) {
					// const alert = await this.alertController.create({
					//   header: 'Erreur lors de la Modification du Mot de Passe',
					//   message: `La Modification du Mot de Passe a échoué (erreur ${res.statusText})`,
					//   buttons: ['Ok'],
					// });

					// await alert.present();
					return;
				}

				this.authentication.login(res.email, password)
					.subscribe(() => {
						window.location.reload();
					});
			});
	}

}
