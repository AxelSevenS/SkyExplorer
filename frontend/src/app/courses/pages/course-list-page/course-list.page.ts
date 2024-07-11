import { Component } from '@angular/core';
import { OnInit } from '@angular/core/index';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { UserRoles } from '../../../users/models/user.model';
import { Course } from '../../models/course.model';
import { CourseService } from '../../services/course.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'se-course-list-page',
  templateUrl: 'course-list.page.html',
  styleUrls: ['course-list.page.scss'],
})
export class CourseListPage implements OnInit {
	UserRoles = UserRoles;

	courses?: Course[];

	constructor(
		public authentication: AuthenticationService,
		private courseService: CourseService
	) {
	}


	ngOnInit(): void {
		if (this.authentication.user && this.authentication.user.role >= UserRoles.Staff) {
			this.courseService.getAll()
				.subscribe(res => {
					if (res instanceof HttpErrorResponse) return;
					this.courses = res;
				});
		}
	}
}
