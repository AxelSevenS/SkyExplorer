import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { User, UserCreateDto, UserRoles, UserUpdateDto } from '../models/user.model';
import { Observable, catchError, of, share } from 'rxjs';
import { EntityService } from '../../core/services/entity.service';

@Injectable({
	providedIn: 'root'
})
export class UserService extends EntityService<User, UserCreateDto, UserUpdateDto> {

	constructor(
		protected override http: HttpClient
	) {
		super(http);
	}


	protected override getEndpointSuffix(): string {
		return 'users'
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

	authenticateUserByEmailAndPassword(email: string, password: string): Observable<string | HttpErrorResponse> {
		const formData = new FormData();
		formData.append('Email', email);
		formData.append('Password', password);

		const headers = new HttpHeaders({ 'enctype': 'multipart/form-data' });

		return this.http.post<string>(`${this.getEndpoint()}/auth`, formData, {headers: headers})
			.pipe(
				share(),
				catchError( (err: HttpErrorResponse) => of(err) ),
			);
	}
}