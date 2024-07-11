import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Course, CourseCreateDto, CourseUpdateDto } from '../../models/course.model';
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
import { EntityViewComponent } from '../../../core/components/entity-view/entity-view.component';
import {MatSpinner} from '@angular/material/progress-spinner';@Component({
	selector: 'se-course-page',
	templateUrl: 'course.page.html',
	styleUrls: ['course.page.scss'],
})
export class CoursePage extends EntityViewComponent<Course, CourseCreateDto, CourseUpdateDto> {
	moment = moment;
	UserRoles = UserRoles;


	editCourseForm: FormGroup = this.formBuilder.group(
		{
			courseName: [''],
			user: [''],
			overseer: [''],
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
	users: User[] = [];
	overseers?: User[];

	wasAcquitted: boolean = false;
	planeId?: number;
	userId?: number;
	overseerId?: number;


	constructor(
		private formBuilder: FormBuilder,
		protected router: Router,
		private activatedRoute: ActivatedRoute,

		public override authentication: AuthenticationService,
		private userService: UserService,
		private planeService: PlaneService,
		private courseService: CourseService,
		private billService: BillService,
		private flightService: FlightService,
	) {
		super(authentication, courseService);
	}



	override ngOnInit(): void {
		super.ngOnInit();

		this.planeService.getAll()
			.subscribe(res => {
				if (res instanceof HttpErrorResponse) return;
				this.planes = res;
			});

		this.userService.getByRole(UserRoles.User)
			.subscribe(res => {
				if (res instanceof HttpErrorResponse) return;
				this.users = res;
			});


		if (this.authentication.user && this.authentication.user.role == UserRoles.Collaborator) {
			this.overseerId = this.authentication.user.id;
		}
		else {
			this.userService.getByRole(UserRoles.Collaborator)
				.subscribe(res => {
					if (res instanceof HttpErrorResponse) return;
					this.overseers = res;
				});
		}

		this.router.events
			.subscribe(e => {
				this.id = this.activatedRoute.snapshot.params['id'];
			});
	}

	protected override onUpdate(): void {
		this.editCourseForm.controls['courseName'].setValue(this.entity?.name);
		this.editCourseForm.controls['user'].setValue(this.userId = this.entity?.flight.user.id);
		this.editCourseForm.controls['overseer'].setValue(this.overseerId = this.entity?.flight.overseer.id);
		this.editCourseForm.controls['plane'].setValue(this.planeId = this.entity?.flight.plane.id);
		this.editCourseForm.controls['date'].setValue(this.entity?.flight.dateTime);
		this.editCourseForm.controls['time'].setValue(moment(this.entity?.flight.dateTime).format("HH:mm"));
		this.editCourseForm.controls['duration'].setValue(this.entity?.flight.duration.substring(0, 5));
		this.editCourseForm.controls['billName'].setValue(this.entity?.flight.bill.name);
		this.editCourseForm.controls['billUrl'].setValue(this.entity?.flight.bill.url);
		this.editCourseForm.controls['wasAcquitted'].setValue(this.wasAcquitted = this.entity?.flight.bill.wasAcquitted ?? false);
	}



	onSubmit(): void {
		if ( ! this.entity ) return;
		if ( ! this.editCourseForm.valid ) return;

		if ( ! this.authentication.user ) return;
		if ( this.authentication.user.id !== this.entity.flight.user.id && this.authentication.user.id !== this.entity.flight.overseer.id && this.authentication.user.role < UserRoles.Staff ) return;


		if (this.authentication.user.id === this.entity.flight.user.id || this.authentication.user.role >= UserRoles.Staff) {
			const updatedBill = new BillUpdateDto();

			const billNameInput: string = this.editCourseForm.controls['billName'].value;
			if (billNameInput !== this.entity.flight.bill.name) {
				updatedBill.name = billNameInput;

			}
			const billUrlInput: string = this.editCourseForm.controls['billUrl'].value;
			if (billUrlInput !== this.entity.flight.bill.url) {
				updatedBill.url = billUrlInput;
			}

			const billWasAcquittedInput: boolean = this.editCourseForm.controls['wasAcquitted'].value;
			if (billWasAcquittedInput !== this.entity.flight.bill.wasAcquitted) {
				updatedBill.wasAcquitted = billWasAcquittedInput;
			}

			if (updatedBill.name !== undefined || updatedBill.url !== undefined || updatedBill.wasAcquitted !== undefined) {
				this.billService.updateById(this.entity.flight.bill.id, updatedBill)
					.subscribe(async bill => {
						if (bill instanceof HttpErrorResponse || ! this.entity) return;
						this.entity.flight.bill = bill;
					})
			}
		}



		const updatedFlight = new FlightUpdateDto();

		const overseerInput: number = this.editCourseForm.controls['overseer'].value;
		if (overseerInput !== this.entity.flight.overseer.id) {
			updatedFlight.overseerId = overseerInput;
		}

		const dateInput: Date = new Date(this.editCourseForm.controls['date'].value);
		const timeInput: string = this.editCourseForm.controls['time'].value;
		if (dateInput && timeInput) {
			const timeSplit = timeInput.substring(0, 5).split(':');
			dateInput.setHours(parseInt(timeSplit[0]));
			dateInput.setMinutes(parseInt(timeSplit[1]));
			dateInput.setSeconds(0);
			dateInput.setMilliseconds(0);
			if (dateInput.toJSON() != new Date(this.entity.flight.dateTime).toJSON()) {
				updatedFlight.dateTime = dateInput.toJSON();
			}
		}

		const durationInput: string = this.editCourseForm.controls['duration'].value;
		if (durationInput !== this.entity.flight.duration) {
			updatedFlight.duration = durationInput;
		}

		if (updatedFlight.overseerId !== undefined || updatedFlight.dateTime !== undefined || updatedFlight.duration !== undefined) {
			this.flightService.updateById(this.entity.flight.id, updatedFlight)
				.subscribe(async flight => {
					console.log(flight);
					if (flight instanceof HttpErrorResponse || ! this.entity) return;
					this.entity.flight = flight;
				});
		}



		const updatedCourse = new CourseUpdateDto();

		const nameInput: string = this.editCourseForm.controls['courseName'].value;
		if (nameInput !== this.entity.name) {
			updatedCourse.name = nameInput;
		}

		if (updatedCourse.name !== undefined) {
			this.courseService.updateById(this.entity.id, updatedCourse)
				.subscribe(async course => {
					if (course instanceof HttpErrorResponse || ! this.entity) return;
					this.entity = course;
				});
		}
	}
}
