import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { User, UserRoles } from '../../../users/models/user.model';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../users/services/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Plane } from '../../../planes/models/plane.model';
import { PlaneService } from '../../../planes/services/course.service';
import { FlightService } from '../../../flights/services/flight.service';
import { CourseService } from '../../services/course.service';
import { FlightCreateDto } from '../../../flights/models/flight.model';
import { BillService } from '../../../bills/services/bill.service';
import { BillCreateDto } from '../../../bills/models/bill.model';
import { CourseCreateDto } from '../../models/course.model';

@Component({
  selector: 'se-create-course-page',
  templateUrl: './create-course.page.html',
  styleUrl: './create-course.page.scss'
})
export class CreateCoursePage implements OnInit {

	UserRoles = UserRoles;

	publishForm: FormGroup = this.formBuilder.group(
		{
			courseName: [''],
			student: [''],
			teacher: [''],
			plane: [''],
			date: [''],
			time: [''],
			duration: [''],
		}, {
		}
	);

	planes: Plane[] = [];
	students: User[] = [];
	teachers?: User[];

	planeId?: number;
	studentId?: number;
	teacherId?: number;

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,

		public authentication: AuthenticationService,
		private userService: UserService,
		private planeService: PlaneService,
		private courseService: CourseService,
		private billService: BillService,
		private flightService: FlightService,
	) {
	}

	ngOnInit(): void {
		if (! this.authentication.user || this.authentication.user.role < UserRoles.Collaborator) {
			this.router.navigateByUrl(document.referrer);
			return;
		}

		this.planeService.getAll()
			.subscribe(res => {
				if (res instanceof HttpErrorResponse) return;
				this.planes = res;
			});

		this.userService.getByRole(UserRoles.User)
			.subscribe(res => {
				if (res instanceof HttpErrorResponse) return;
				this.students = res;
			});


		if (this.authentication.user && this.authentication.user.role == UserRoles.Collaborator) {
			this.teacherId = this.authentication.user.id;
		}
		else {
			this.userService.getByRole(UserRoles.Collaborator)
				.subscribe(res => {
					if (res instanceof HttpErrorResponse) return;
					this.teachers = res;
				});
		}
	}


	public publish(): void {
		if (this.publishForm.invalid) {
			console.log(this.publishForm);
			return;
		}


		const date: Date = new Date(this.publishForm.controls["date"].value);
		const time: string = this.publishForm.controls["time"].value;
		const duration: string = this.publishForm.controls["duration"].value;

		const timeSplit = time.split(':');
		date.setHours(parseInt(timeSplit[0]));
		date.setMinutes(parseInt(timeSplit[1]));


		let billRequest = new BillCreateDto(
			this.publishForm.controls["url"].value,
			"name",
			false,
		);

		this.billService.create(billRequest)
			.subscribe(bill => {
				if (bill instanceof HttpErrorResponse) return;

				let flightRequest = new FlightCreateDto(
					this.publishForm.controls["student"].value,
					this.publishForm.controls["teacher"].value,
					bill.id,
					this.publishForm.controls["plane"].value,
					duration,
					date.toJSON(),
				);

				this.flightService.create(flightRequest)
					.subscribe(flight => {
						if (flight instanceof HttpErrorResponse) return;

						let courseRequest = new CourseCreateDto(
							this.publishForm.controls["courseName"].value,
							flight.id,
							"", // this.publishForm.controls["goals"].value,
							"", // this.publishForm.controls["notes"].value,
							"", // this.publishForm.controls["acquiredSkills"].value,
						);

						this.courseService.create(courseRequest)
						.subscribe(course => {
							if (course instanceof HttpErrorResponse) return;

							this.router.navigate([`/courses/${course.id.toString()}`])
						});
					});
			});
	}

// 	public createActivity(): void {
// 		if (!this.studentId || !this.teacherId || !this.planeId) return;

// 		let billRequest = new BillCreateDto(
// 			"url",
// 			"name",
// 			false,
// 		);

// 		this.billService.create(billRequest)
// 			.subscribe(bill => {
// 				if (!this.studentId || !this.teacherId || !this.planeId) return;
// 				if (bill instanceof HttpErrorResponse) return;

// 				let flightRequest = new FlightCreateDto(
// 					this.studentId,
// 					this.teacherId,
// 					bill.id,
// 					this.planeId,

// 					new Date(this.publishForm.controls["date"].value),
// 					new Date("1970T" + this.publishForm.controls["duration"].value),
// 				);

// 				this.flightService.create(flightRequest)
// 					.subscribe(flight => {
// 						if (flight instanceof HttpErrorResponse) return;

// 						// let courseRequest = new CourseCreateDto(
// 						// 	this.publishForm.controls["courseName"].value,

// 						// );

// 						this.activityService.create(courseRequest)
// 							.subscribe(course => {
// 								if (course instanceof HttpErrorResponse) return;

// 								console.log(course)
// 							});
// 					});
// 			});
// 	}
}
