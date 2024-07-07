import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { UserRoles } from '../../../users/models/user.model';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticationValidators } from '../../../authentication/validators/authentication-validators';

@Component({
  selector: 'se-create-course-page',
  templateUrl: './create-course.page.html',
  styleUrl: './create-course.page.scss'
})
export class CreateCoursePage implements OnInit {

	publishForm: FormGroup = this.formBuilder.group(
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
		private authentication: AuthenticationService,
		private router: Router,
		private formBuilder: FormBuilder,
	) {

	}

	ngOnInit(): void {
		if (! this.authentication.user || this.authentication.user.role < UserRoles.Collaborator) {
			this.router.navigateByUrl(document.referrer);
		}
	}

	publish(): void {

	}

}
