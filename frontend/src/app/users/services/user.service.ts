import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { User, UserCreateDto, UserRoles, UserUpdateDto } from '../models/user.model';
import { Observable, catchError, of, share } from 'rxjs';
import { EntityService } from '../../core/services/entity.service';

@Injectable({
	providedIn: 'root'
})
export class UserService extends EntityService<User, UserCreateDto, UserUpdateDto> {
	protected override get endpointSuffix(): string { return 'users' }


	constructor(
		protected override http: HttpClient
	) {
		super(http);
	}


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

	getByRole(role: UserRoles): Observable<User[] | HttpErrorResponse> {
		return this.http.get<User[]>(`${this.endpoint}/byRole/${role}`)
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}

	authenticateUserByEmailAndPassword(email: string, password: string): Observable<string | HttpErrorResponse> {
		const formData = new FormData();
		formData.append('Email', email);
		formData.append('Password', password);

		const headers = new HttpHeaders({ 'enctype': 'multipart/form-data' });

		return this.http.post<string>(`${this.endpoint}/auth`, formData, {headers: headers})
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}
}