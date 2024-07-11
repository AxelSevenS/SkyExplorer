import { Component } from '@angular/core';
import { UserService } from '../../users/services/user.service';
import { CourseService } from '../../courses/services/course.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { AuthenticationService } from '../../authentication/services/authentication.service';
import { Course } from '../../courses/models/course.model';
import { UserRoles } from '../../users/models/user.model';

@Component({
	selector: 'se-course-list-page',
	templateUrl: 'dashboard.page.html',
	styleUrls: ['dashboard.page.scss'],
})
export class DashboardPage {

	upcomingCourses: Course[] = [];
	nextCourse: any;


	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		//private dashboardService: DashboardService,

		private authentication: AuthenticationService,
		private courseService: CourseService,
		private userService: UserService,
	) {

	}


	ngOnInit() {
		if (! this.authentication.user) return;

		const courseObservable = this.authentication.user.role >= UserRoles.Staff ?
			this.courseService.getAll() :
			this.courseService.getForUser(this.authentication.user.id, 0, "AllTime", "Future");

		if (courseObservable) {
			courseObservable
				.subscribe((res) => {
					if (Array.isArray(res) && res.length > 0) {
						this.upcomingCourses = res.slice(0, 3);
					}
				});
		}
	}
}
