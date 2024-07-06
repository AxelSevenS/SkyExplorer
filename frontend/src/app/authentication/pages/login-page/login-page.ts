import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { AuthenticationValidators } from '../../validators/authentication-validators';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
	selector: 'se-login-page',
	templateUrl: './login-page.html',
	styleUrls: ['./login-page.scss'],
})
export class LoginPage implements OnInit {

	loginForm: FormGroup = this.formBuilder.group({
		email: [''],
		password: [''],
	});

	registerForm: FormGroup = this.formBuilder.group(
		{
			email: ['', Validators.email],
			firstName: [''],
			lastName: [''],
			password: ['', AuthenticationValidators.securePasswordValidator],
			passwordConfirm: ['', AuthenticationValidators.securePasswordValidator],
		}, {
			validators: [
				AuthenticationValidators.confirmPasswordValidator('password', 'passwordConfirm')
			]
		}
	);

	constructor(
		private formBuilder: FormBuilder,
		private authenticationService: AuthenticationService,
		private router: Router
	) {}

	ngOnInit() {}

	login() {
		this.authenticationService.login(this.loginForm.controls["email"].value, this.loginForm.controls["password"].value)
			.subscribe(async res => {
				if (res instanceof HttpErrorResponse) {
					// const alert = await this.alertController.create({
					//   header: 'Erreur lors de la Connexion',
					//   message: `La Connexion a échoué (erreur ${res.statusText})`,
					//   buttons: ['Ok'],
					// });

					// await alert.present();
					return;
				}

				this.router.navigate([''])
					.then(() => window.location.reload())
			})
	}

	register() {
		let email = this.registerForm.controls["email"].value;
		let password = this.registerForm.controls["password"].value;
		let firstName = this.registerForm.controls["firstName"].value;
		let lastName = this.registerForm.controls["lastName"].value;
		this.authenticationService.register(email, password, firstName, lastName)
			.subscribe(async res => {
				if (res instanceof HttpErrorResponse) {
					// const alert = await this.alertController.create({
					//   header: 'Erreur lors de l\'Inscription',
					//   message: `L\'Inscription a échoué (erreur ${res.statusText})`,
					//   buttons: ['Ok'],
					// });

					// await alert.present();
					return;
				}

				this.authenticationService.login(email, password)
					.subscribe(() => {
						this.router.navigate([''])
							.then(() => window.location.reload())
					})
			})
	}

}
