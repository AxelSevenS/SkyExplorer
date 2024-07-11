import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Course, CourseCreateDto, CourseUpdateDto } from '../models/course.model';
import { Observable, catchError, of, share } from 'rxjs';
import { EntityService } from '../../core/services/entity.service';

type TimeFrames = "AllTime" | "Daily" | "Weekly" | "Monthly" | "Yearly";
type DateFrames = "AllTime" | "Today" | "Future" | "Past";

@Injectable({
	providedIn: 'root'
})
export class CourseService extends EntityService<Course, CourseCreateDto, CourseUpdateDto> {

	protected override get endpointSuffix(): string { return 'courses' }



	constructor(
		protected override http: HttpClient
	) {
		super(http);
	}

	getWithQuery(userId: number, offset: number = 0, timeFrame: TimeFrames = "AllTime", dateFrame: DateFrames = "AllTime"): Observable<Course[] | HttpErrorResponse> {
		let url = new URL(this.endpoint);
		url.searchParams.append('TimeFrame', timeFrame);
		url.searchParams.append('DateFrame', dateFrame);
		url.searchParams.append('Offset', offset.toString());

		return this.http.get<Course[]>( url.toString() )
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}

	getForUser(userId: number, offset: number = 0, timeFrame: TimeFrames = "AllTime", dateFrame: DateFrames = "AllTime"): Observable<Course[] | HttpErrorResponse> {
		let url = new URL(`${this.endpoint}/user/${userId}`);
		url.searchParams.append('TimeFrame', timeFrame);
		url.searchParams.append('DateFrame', dateFrame);
		url.searchParams.append('Offset', offset.toString());

		return this.http.get<Course[]>( url.toString() )
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}
}