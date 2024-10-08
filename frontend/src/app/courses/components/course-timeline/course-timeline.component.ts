import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Course } from '../../models/course.model';
import { UserRoles } from '../../../users/models/user.model';


@Component({
	selector: 'se-course-timeline',
	templateUrl: './course-timeline.component.html',
	styleUrls: ['./course-timeline.component.scss']
})
export class CourseTimelineComponent implements OnInit {
	hours: number[] = [8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];

	data: { [key in number]: PeriodCourseData} = {};

	public get displayMode() { return this._displayMode; }
	public set displayMode(value: CoursePeriod) {
		this._displayMode = value;
		this.updateTimelineData();
	}
	private _displayMode: CoursePeriod = 'Weekly';


	get timelineOffset() { return this._timelineOffset; }
	set timelineOffset(value: number) {
		this._timelineOffset = value;
		this.updateTimelineData();
	}
	_timelineOffset: number = 0;



	constructor(
		private authentication: AuthenticationService,
		private courseService: CourseService
	) {
		this.updateTimelineData();
	}

	ngOnInit(): void {
		this.courseService.eventRemoved
			.subscribe(entity => {
				this.updateTimelineData();
			});

		this.courseService.eventUpdated
			.subscribe(entity => {
				this.updateTimelineData();
			});

		this.courseService.eventAdded
			.subscribe(entity => {
				this.updateTimelineData();
			});
	}

	updateTimelineData() {
		if (! this.authentication.user) return;

		if (this.data[this.timelineOffset] && this.data[this.timelineOffset].type == this.displayMode) return;


		this.data[this._timelineOffset] = {
			type: this.displayMode,
			days: [],
		};
		const courseData = this.data[this._timelineOffset];
		let tempData: PeriodCourseData = {
			type: this.displayMode,
			days: [],
		};

		let today = new Date(Date.now());


		let timeFrame = 7;
		let timeBasis = 0;

		if (this._displayMode == 'Weekly') {
			today.setDate(today.getDate() + this.timelineOffset * 7);
			timeFrame = 7;
			timeBasis = (today.getDay() + 6) % 7;
		}
		else if (this._displayMode == 'Monthly') {
			today.setMonth(today.getMonth() + this.timelineOffset);

			let endOfMonth = new Date(today.getFullYear(), today.getMonth() + 1, 0);

			timeFrame = endOfMonth.getDate();
			timeBasis = today.getDate() - 1;
		}

		// Get the dates for the whole period
		for (let day = 0; day < timeFrame; day++) {

			let date = new Date(today);
			let weekDayOffset = day - timeBasis; // Get offset from today's day to the desired day

			date.setDate(today.getDate() + weekDayOffset);

			courseData.days[day] = {
				date: date,
				courses: []
			};

			tempData.days[day] = {
				date: date,
				courses: []
			}
		}



		// get the course data for the week
		this.courseService.getForUser(this.authentication.user.id, this.timelineOffset, 'Weekly', )
			.subscribe(courses => {
				if (courses instanceof HttpErrorResponse) return;

				// Setup for caching, get all the course data in a legible state
				for (let index = 0; index < courses.length; index++) {
					const course = courses[index];
					const dateTime = new Date(course.flight.dateTime);
					const duration = new Date("1970-01-01T" + course.flight.duration);

					const day = (dateTime.getDay() + 6) % 7; // Get weekday in Monday-first time
					let time = dateTime.getHours();

					if (time > this.hours[this.hours.length - 1]) continue;
					if (time < this.hours[0]) {
						const diff = this.hours[0] - time;
						time += diff;
						duration.setHours(duration.getHours() - diff);
					}

					tempData.days[day].courses[time] = {
						course: course,
						duration: Math.min(duration.getHours(), this.hours.length - this.hours.indexOf(time)),
						hidden: false,
					};
				}

				// cache the data in a per-hour basis
				for (let day = 0; day < tempData.days.length; day++) {
					const dayCourses = tempData.days[day].courses;

					for (let hour = dayCourses.length - 1; hour > 0; hour--) {
						const hourCourse = dayCourses[hour];
						if (! hourCourse) continue;
						console.log(hourCourse);

						const durationEnd = Math.min(hourCourse.duration, this.hours[this.hours.length - 1] - hour + 1);
						for (let travellingDuration = 0; travellingDuration < durationEnd; travellingDuration++) {
							courseData.days[day].courses[hour + travellingDuration] = {
								course: hourCourse.course,
								duration: hourCourse.duration - travellingDuration,
								hidden: travellingDuration !== 0,
							}
						}

						const nextCourse = courseData.days[day].courses[hour + hourCourse.duration];
						if (nextCourse) {
							nextCourse.hidden = false;
						}
					}
				}
			})
	}

	getCourseAtTime(day: number, time: number): CourseData | null {
		const dayData = this.data[this._timelineOffset].days[day];
		if (! dayData) return null;

		return dayData.courses[time];
	}

	// if (potentialIntersector && potentialIntersector.duration - 1 >= time - index) {
	// 	if (index + potentialIntersector.duration > time) {
	// 		// If a previous course overlaps the current time but is not exactly at the current time
	// 		return {
	// 			...potentialIntersector,
	// 			duration: index + potentialIntersector.duration - time,
	// 			hidden: false
	// 		};
	// 	}
	// }
}

type PeriodCourseData = {
	type: CoursePeriod,
	days: {
		date: Date;
		courses: CourseData[];
	}[]
};

type CourseData = {
	hidden: boolean,
	course: Course;
	duration: number;
};

export type CoursePeriod = 'Weekly' | 'Monthly';