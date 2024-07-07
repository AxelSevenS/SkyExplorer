import { Component } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Course } from '../../models/course.model';

@Component({
  selector: 'se-course-timeline',
  templateUrl: './course-timeline.component.html',
  styleUrls: ['./course-timeline.component.scss']
})
export class CourseTimelineComponent {
  weekDays = Object.values(WeekDay);
  hours: number[] = [8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];

  courses: { [day in WeekDay]: { [hour: number]: { name: string, duration: number } } } = {
		[WeekDay.Monday]: {},
		[WeekDay.Tuesday]: {},
		[WeekDay.Wednesday]: {},
		[WeekDay.Thursday]: {},
		[WeekDay.Friday]: {},
		[WeekDay.Saturday]: {},
		[WeekDay.Sunday]: {}
	};

	constructor(
		private authentication: AuthenticationService,
		private course: CourseService
	) {
		if (! authentication.user) return;
		course.getWeekly(authentication.user.id)
		.subscribe(async res => {
			if (res instanceof HttpErrorResponse) {
				// const alert = await this.alertController.create({
				//   header: 'Erreur lors de la Connexion',
				//   message: `La Connexion a échoué (erreur ${res.statusText})`,
				//   buttons: ['Ok'],
				// });

				// await alert.present();
				return;
			}

			this.setTimelineData(res);
		})
	}

	private setTimelineData(courses: Course[]) {
		for (let index = 0; index < courses.length; index++) {
			const course = courses[index];

			const dateTime = new Date(course.flight.dateTime);
			const duration = new Date("1970-01-01T" + course.flight.duration);

			const day = (dateTime.getDay() + 6) % 7;
			const time = dateTime.getHours();

			const dayIndex = Object.values(WeekDay)[day];

			if (time < 8 || time > 18) continue;

			this.courses[dayIndex][time] = {
				name: course.flight.plane.name,
				duration: duration.getHours()
			};
		}
	}

  getCourseAtTime(day: WeekDay, time: number): { name: string; duration: number; } | null {
    const dayCourses = this.courses[day];
		if (! dayCourses) return null;

		const hourCourse = dayCourses[time];

    return hourCourse ? hourCourse : null;
  }

  isCourseOccupied(day: WeekDay, time: number): boolean {
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

export enum WeekDay {
  Monday = 'Lundi',
  Tuesday = 'Mardi',
  Wednesday = 'Mercredi',
  Thursday = 'Jeudi',
  Friday = 'Vendredi',
  Saturday = 'Samedi',
  Sunday = 'Dimanche'
}