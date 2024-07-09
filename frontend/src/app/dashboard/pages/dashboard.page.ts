import { Component } from '@angular/core';
import { UserService } from '../../users/services/user.service';
import { CourseService } from '../../courses/services/course.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { AuthenticationService } from '../../authentication/services/authentication.service';

@Component({
	selector: 'se-course-list-page',
	templateUrl: 'dashboard.page.html',
	styleUrls: ['dashboard.page.scss'],
})
export class DashboardPage {

	nextCourses: any[] = [];
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

		this.courseService.getWeeklyForStudent(this.authentication.user.id, 0).subscribe((res) => {
			if (Array.isArray(res) && res.length > 0) {
				this.nextCourses = res.slice(0, 3);
			}
		});
	}

	displayNextCourse() {
		for (let i = 0; i < this.nextCourses.length; i++) {
			let nextCourse = this.nextCourses[i];
			let courseName = nextCourse.name;
			let student = nextCourse.student;
			let teacher = nextCourse.teacher;
			let courseDate = nextCourse.date;

		}
	}
}
