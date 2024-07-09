import { Component } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { HttpErrorResponse } from '@angular/common/http';


export enum CoursePeriod {
	Weekly = 'weekly',
	Monthly = 'monthly'
};
@Component({
  selector: 'se-course-timeline',
  templateUrl: './course-timeline.component.html',
  styleUrls: ['./course-timeline.component.scss']
})
export class CourseTimelineComponent {
	DisplayMode = CoursePeriod;
  hours: number[] = [8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];

  data: { [key in number]: PeriodCourseData} = {};

	public get displayMode() { return this._displayMode; }
	public set displayMode(value: CoursePeriod) {
		this._displayMode = value;
		this.setTimelineData();
	}
	private _displayMode: CoursePeriod = CoursePeriod.Weekly;


	get timelineOffset() { return this._timelineOffset; }
	set timelineOffset(value: number) {
		this._timelineOffset = value;
		this.setTimelineData(true);
	}
	_timelineOffset: number = 0;



	constructor(
		private authentication: AuthenticationService,
		private course: CourseService
	) {
		this.setTimelineData();
	}

	setTimelineData(force: boolean = false) {
		if (! this.authentication.user) return;

		if (this.data[this.timelineOffset] && this.data[this.timelineOffset].type == this.displayMode) return;


		this.data[this._timelineOffset] = {
			type: this.displayMode,
			days: []
		};
		const courseData = this.data[this._timelineOffset];

		let today = new Date(Date.now());


		// today.setDate(today.getDate() + (offset * 7))
		// const todayWeekDay = (today.getDay() + 6) % 7; // Get weekday in Monday-first time
		let timeFrame = 7;
		let timeBasis = 0;

		if (this._displayMode == CoursePeriod.Weekly) {
			today.setDate(today.getDate() + this.timelineOffset * 7);
			timeFrame = 7;
			timeBasis = (today.getDay() + 6) % 7;
		}
		else if (this._displayMode == CoursePeriod.Monthly) {
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
		}

		// get the course data for the week
		this.course.getWeeklyForStudent(this.authentication.user.id, this.timelineOffset)
			.subscribe(courses => {
				if (courses instanceof HttpErrorResponse) return;

				for (let index = 0; index < courses.length; index++) {
					const course = courses[index];
					const dateTime = new Date(course.flight.dateTime);
					const duration = new Date("1970-01-01T" + course.flight.duration);

					const day = (dateTime.getDay() + 6) % 7; // Get weekday in Monday-first time
					const time = dateTime.getHours();

					if (time < this.hours[0] || time > this.hours[this.hours.length - 1]) continue;

					courseData.days[day].courses[time] = {
						name: course.name,
						plane: course.flight.plane.name,
						duration: duration.getHours()
					};
				}
			})
	}

	getDuration(maxDuration: number, hour: number): number {
		return Math.min(maxDuration, this.hours.length - this.hours.indexOf(hour))
	}

  getCourseAtTime(day: number, time: number): CourseData | null {
    const dayData = this.data[this._timelineOffset].days[day];
		if (! dayData) return null;

		const hourCourse = dayData.courses[time];

    return hourCourse ? hourCourse : null;
  }

  isCourseOccupied(day: number, time: number): boolean {
		for (let index = time - 1; index > 0; index--) {
			let course = this.getCourseAtTime(day, index);
			if (course && course.duration - 1 >= time - index) {
				return true;
			}
		}
		return false;
  }

	stringToColor(str: string): string {
    let hash = 0;
    for (var i = 0; i < str.length; i++) {
			hash = str.charCodeAt(i) + ((hash << 15) - hash);
    }
    let colour = '#';
    for (let i = 0; i < 3; i++) {
			let value = (hash >> (i * 8)) & 0xFF;
			colour += ('00' + value.toString(16)).substr(-2);
    }
    return colour;
	}
}

type PeriodCourseData = {
	type: CoursePeriod,
	days: {
		date: Date;
		courses: CourseData[];
	}[]
};

type CourseData = {
	name: string;
	plane: string;
	duration: number;
};