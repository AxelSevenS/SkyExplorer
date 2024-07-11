import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Course, CourseUpdateDto } from '../../models/course.model';
import { CourseService } from '../../services/course.service';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { User, UserRoles } from '../../../users/models/user.model';
import { Plane } from '../../../planes/models/plane.model';
import { UserService } from '../../../users/services/user.service';
import { PlaneService } from '../../../planes/services/plane.service';
import { BillService } from '../../../bills/services/bill.service';
import { FlightService } from '../../../flights/services/flight.service';
import moment from 'moment';
import { FlightUpdateDto } from '../../../flights/models/flight.model';
import { BillUpdateDto } from '../../../bills/models/bill.model';

@Component({
	selector: 'se-course-page',
	templateUrl: 'course.page.html',
	styleUrls: ['course.page.scss'],
})
export class CoursePage {
	moment = moment;

	UserRoles = UserRoles;

	editCourseForm: FormGroup = this.formBuilder.group(
		{
			courseName: [''],
			student: [''],
			teacher: [''],
			plane: [''],
			date: [''],
			time: [''],
			duration: [''],
			goals: [''],
			achievedGoals: [''],
			notes: [''],
			billName: [''],
			billUrl: [''],
			wasAcquitted: [''],
		}
	);

	planes: Plane[] = [];
	students: User[] = [];
	teachers?: User[];

	wasAcquitted: boolean = false;
	planeId?: number;
	studentId?: number;
	teacherId?: number;


	public get requestId(): number { return this.activatedRoute.snapshot.params['id'] }

	public get course() { return this._course }
	private _course?: Course | null;


	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private activatedRoute: ActivatedRoute,

		public authentication: AuthenticationService,
		private userService: UserService,
		private planeService: PlaneService,
		private courseService: CourseService,
		private billService: BillService,
		private flightService: FlightService,
	) {
	}

	ngOnInit(): void {
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


		this.updateCourse();

		this.courseService.eventRemoved
			.subscribe(course => {
				if (this._course?.id != course.id) return;
				this._course = null;
			});

		this.courseService.eventUpdated
			.subscribe(course => {
				if (this._course?.id != course.id) return;
				this._course = course;
			});

		this.router.events
			.subscribe(e => {
				this.updateCourse();
			});
	}

	private updateCourse(): void {
		if (this.course?.id === this.requestId) return;

		this.courseService.getById(this.requestId)
			.subscribe(course => {
				if (course instanceof HttpErrorResponse) return;

				this._course = course;

				this.editCourseForm.controls['courseName'].setValue(course.name);
				this.editCourseForm.controls['student'].setValue(course.flight.user.id);
				this.editCourseForm.controls['teacher'].setValue(course.flight.overseer.id);
				this.editCourseForm.controls['date'].setValue(course.flight.dateTime);
				this.editCourseForm.controls['time'].setValue(moment(course.flight.dateTime).format("HH:mm"));
				this.editCourseForm.controls['duration'].setValue(course.flight.duration.substring(0, 5));
				this.editCourseForm.controls['billName'].setValue(course.flight.bill.name);
				this.editCourseForm.controls['billUrl'].setValue(course.flight.bill.url);
				this.editCourseForm.controls['wasAcquitted'].setValue(course.flight.bill.wasAcquitted);
			});
	}


	onSubmit(): void {
		if ( ! this.course ) return;
		if ( ! this.editCourseForm.valid ) return;


		const updatedBill = new BillUpdateDto();

		const billNameInput: string = this.editCourseForm.controls['billName'].value;
		if (billNameInput !== this.course.flight.bill.name) {
			updatedBill.name = billNameInput;

		}
		const billUrlInput: string = this.editCourseForm.controls['billUrl'].value;
		if (billUrlInput !== this.course.flight.bill.url) {
			updatedBill.url = billUrlInput;
		}

		const billWasAcquittedInput: boolean = this.editCourseForm.controls['wasAcquitted'].value;
		if (billWasAcquittedInput !== this.course.flight.bill.wasAcquitted) {
			updatedBill.wasAcquitted = billWasAcquittedInput;
		}

		if (updatedBill.name !== undefined || updatedBill.url !== undefined || updatedBill.wasAcquitted !== undefined) {
			this.billService.updateById(this.course.flight.bill.id, updatedBill)
				.subscribe(async bill => {
					if (bill instanceof HttpErrorResponse || ! this.course) return;
					this.course.flight.bill = bill;
				})
		}



		const updatedFlight = new FlightUpdateDto();

		const teacherInput: number = this.editCourseForm.controls['teacher'].value;
		if (teacherInput !== this.course.flight.overseer.id) {
			updatedFlight.overseerId = teacherInput;
		}

		const dateInput: Date = new Date(this.editCourseForm.controls['date'].value);
		const timeInput: string = this.editCourseForm.controls['time'].value;
		if (dateInput && timeInput) {
			const timeSplit = timeInput.substring(0, 5).split(':');
			dateInput.setHours(parseInt(timeSplit[0]));
			dateInput.setMinutes(parseInt(timeSplit[1]));
			dateInput.setSeconds(0);
			dateInput.setMilliseconds(0);
			if (dateInput.toJSON() != new Date(this.course.flight.dateTime).toJSON()) {
				updatedFlight.dateTime = dateInput.toJSON();
			}
		}

		const durationInput: string = this.editCourseForm.controls['duration'].value;
		if (durationInput !== this.course.flight.duration) {
			updatedFlight.duration = durationInput;
		}

		if (updatedFlight.overseerId !== undefined || updatedFlight.dateTime !== undefined || updatedFlight.duration !== undefined) {
			this.flightService.updateById(this.course.flight.id, updatedFlight)
				.subscribe(async flight => {
					console.log(flight);
					if (flight instanceof HttpErrorResponse || ! this.course) return;
					this.course.flight = flight;
				});
		}



		const updatedCourse = new CourseUpdateDto();

		const nameInput: string = this.editCourseForm.controls['courseName'].value;
		if (nameInput !== this.course.name) {
			updatedCourse.name = nameInput;
		}

		if (updatedCourse.name !== undefined) {
			this.courseService.updateById(this.course.id, updatedCourse)
				.subscribe(async course => {
					if (course instanceof HttpErrorResponse || ! this.course) return;
					this._course = course;
				});
		}
	}


	async delete() {
		if(! this.course) return;

		this.courseService.deleteById(this.course.id)
			.subscribe(async course => {
				if (course instanceof HttpErrorResponse || ! this.course) return;
				this._course = course;
			});
	}

}
