import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { User, UserRoles } from '../models/user.model';
import { Observable, Subject, catchError, map, of, share } from 'rxjs';
import { AuthenticationService } from '../../authentication/services/authentication.service';

export declare type AuthenticationState = 'loggedIn' | 'loggedOut' | 'disconnected';

@Injectable({
	providedIn: 'root'
})
export class UserService {

	public get eventAdded() { return this._eventAdded };
	private _eventAdded: Subject<User> = new Subject<User>;

	public get eventRemoved() { return this._eventRemoved };
	private _eventRemoved: Subject<User> = new Subject<User>;

	public get eventUpdated() { return this._eventUpdated };
	private _eventUpdated: Subject<User> = new Subject<User>;

	constructor(
		private http: HttpClient
	) { }


	getSubserviantRoles(role: UserRoles): UserRoles[] {
		let roles: UserRoles[] = [];

		switch (role) {
			case UserRoles.Admin:
				roles.push(UserRoles.Admin);

			case UserRoles.Staff:
				roles.push(UserRoles.Staff);

			case UserRoles.Collaborator:
				roles.push(UserRoles.Collaborator);

			case UserRoles.User:
				roles.push(UserRoles.User);
				break;

			default:
				break;
		}

		return roles;
	}

	getUsers(): Observable<User[] | HttpErrorResponse> {
		return this.http.get<User[]>(`${environment.host}/api/users`)
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}

	getUserById(id: number): Observable<User | HttpErrorResponse> {
		return this.http.get<User>(`${environment.host}/api/users/${id}`)
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}

	authenticateUserByUsernameAndPassword(email: string, password: string): Observable<string | HttpErrorResponse> {
		const formData = new FormData();
		formData.append('Email', email);
		formData.append('Password', password);

		const headers = new HttpHeaders({ 'enctype': 'multipart/form-data' });

		return this.http.post<string>(`${environment.host}/api/users/auth/`, formData, {headers: headers})
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}

	createUser(email: string, password: string, firstName: string, lastName: string): Observable<User | HttpErrorResponse> {
		const formData = new FormData();
		formData.append('Email', email);
		formData.append('Password', password);
		formData.append('FirstName', firstName);
		formData.append('LastName', lastName);

		const headers = new HttpHeaders({ 'enctype': 'multipart/form-data' });

		let observable = this.http.put<User>(`${environment.host}/api/users/`, formData, {headers: headers})
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

	updateUserById(id: number, user: Partial<User> | undefined, password: string | undefined = undefined): Observable<User | HttpErrorResponse> {
		const formData = new FormData();

		if (user?.email) formData.append('Email', user.email);
		if (user?.firstName) formData.append('FirstName', user.firstName);
		if (user?.lastName) formData.append('LastName', user.lastName);
		if (user?.role) formData.append('Role', user.role);
		if (password) formData.append('Password', password);

		const headers = new HttpHeaders({ 'Authorization': `Bearer ${localStorage.getItem(AuthenticationService.storageKey)}` });

		let observable = this.http.patch<User>(`${environment.host}/api/users/${id}`, formData, {headers: headers})
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

	deleteUserById(id: number): Observable<User | HttpErrorResponse> {
		const headers = new HttpHeaders({ 'Authorization': `Bearer ${localStorage.getItem(AuthenticationService.storageKey)}` });

		let observable = this.http.delete<User>(`${environment.host}/api/users/${id}`, {headers: headers})
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