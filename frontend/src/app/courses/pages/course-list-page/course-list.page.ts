import { Component } from '@angular/core';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { UserRoles } from '../../../users/models/user.model';

@Component({
  selector: 'se-course-list-page',
  templateUrl: 'course-list.page.html',
  styleUrls: ['course-list.page.scss'],
})
export class CourseListPage {
	UserRoles = UserRoles;

  constructor(
		public authentication: AuthenticationService
	) {
	}
}
