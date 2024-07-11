import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Course, CourseCreateDto, CourseUpdateDto } from '../models/course.model';
import { Observable, catchError, of, share } from 'rxjs';
import { EntityService } from '../../core/services/entity.service';

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

	getWeeklyForUser(userId: number, offset: number = 0): Observable<Course[] | HttpErrorResponse> {
		let url = new URL(`${this.endpoint}/user/${userId}`);
		url.searchParams.append('timeFrame', "Weekly");
		url.searchParams.append('offset', offset.toString());

		return this.http.get<Course[]>( url.toString() )
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}

	getWeeklyForTeacher(userId: number, offset: number = 0): Observable<Course[] | HttpErrorResponse> {
		let url = new URL(`${this.endpoint}/teacher/${userId}`);
		url.searchParams.append('timeFrame', "Weekly");
		url.searchParams.append('offset', offset.toString());

		return this.http.get<Course[]>( url.toString() )
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}
}