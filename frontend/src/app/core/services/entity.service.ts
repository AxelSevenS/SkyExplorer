import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, Subject, catchError, map, of, share } from 'rxjs';
import { AuthenticationService } from '../../authentication/services/authentication.service';
import { EntityCreateDto, EntityUpdateDto, IEntity } from '../models/entity.model';

@Injectable({
	providedIn: 'root'
})
export abstract class EntityService<T extends IEntity, TCreateDto extends EntityCreateDto, TUpdateDto extends EntityUpdateDto> {

	protected abstract get endpointSuffix(): string;
	public get endpoint() { return `${environment.host}/api/${this.endpointSuffix}`; }


	public get eventAdded() { return this._eventAdded };
	private _eventAdded: Subject<T> = new Subject<T>;

	public get eventRemoved() { return this._eventRemoved };
	private _eventRemoved: Subject<T> = new Subject<T>;

	public get eventUpdated() { return this._eventUpdated };
	private _eventUpdated: Subject<T> = new Subject<T>;

	protected get bearerAuth(): string { return `Bearer ${localStorage.getItem(AuthenticationService.storageKey)}`; }

	constructor(
		protected http: HttpClient
	) { }




	getAll(): Observable<T[] | HttpErrorResponse> {
		const headers = new HttpHeaders({ 'Authorization': this.bearerAuth });

		return this.http.get<T[]>(this.endpoint, {headers: headers})
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}

	getById(id: number): Observable<T | HttpErrorResponse> {
		const headers = new HttpHeaders({ 'Authorization': this.bearerAuth });

		return this.http.get<T>(`${this.endpoint}/${id}`, {headers: headers})
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}

	create(createRequest: TCreateDto): Observable<T | HttpErrorResponse> {
		const formData = new FormData();
		createRequest.populate(formData);

		const headers = new HttpHeaders({ 'enctype': 'multipart/form-data', 'Authorization': this.bearerAuth });

		let observable = this.http.post<T>(this.endpoint, formData, {headers: headers})
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);

		observable
			.subscribe(res => {
				if (res instanceof HttpErrorResponse) return;
				this._eventAdded.next(res);
			});

		return observable;
	}

	updateById(id: number, updateRequest: TUpdateDto): Observable<T | HttpErrorResponse> {
		const formData = new FormData();
		updateRequest.populate(formData);

		const headers = new HttpHeaders({ 'Authorization': this.bearerAuth });

		let observable = this.http.patch<T>(`${this.endpoint}/${id}`, formData, {headers: headers})
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);

		observable
			.subscribe(res => {
				if (res instanceof HttpErrorResponse) return;
				this._eventUpdated.next(res);
			});

		return observable;
	}

	deleteById(id: number): Observable<T | HttpErrorResponse> {
		const headers = new HttpHeaders({ 'Authorization': this.bearerAuth });

		let observable = this.http.delete<T>(`${this.endpoint}/${id}`, {headers: headers})
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);

		observable
			.subscribe(res => {
				if (res instanceof HttpErrorResponse) return;
				this._eventRemoved.next(res);
			});

		return observable;
	}
}