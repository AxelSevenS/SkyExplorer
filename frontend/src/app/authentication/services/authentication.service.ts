import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { JwtPayload, jwtDecode } from 'jwt-decode';
import { User, UserCreateDto, UserRoles } from '../../users/models/user.model';
import { Router } from '@angular/router';
import { UserService } from '../../users/services/user.service';

export declare type LoginState = 'loggedIn' | 'loggedOut' | 'disconnected';

@Injectable({
	providedIn: 'root'
})
export class AuthenticationService {

	public static readonly storageKey: string = "JWT";

	public get state() { return this._state }
	private _state: LoginState;

	private _user: User | null = null;
	public get user() { return this._user }

	constructor(
		private userService: UserService,
		private router: Router
	) {
		this._state = 'loggedOut';

		let jwt = localStorage.getItem(AuthenticationService.storageKey);
		if (jwt === null) return;

		let user = this.jwtToUser(jwt);
		if (user === null) {
			localStorage.removeItem(AuthenticationService.storageKey);
			return;
		}

		this._user = user;
		this._state = 'loggedIn';

		this.userService.eventRemoved
			.subscribe(res => {
				if (res.id != this._user?.id) return;

				this.logout();
			});

		this.userService.eventUpdated
			.subscribe(res => {
				if (res.id != this._user?.id) return;

				this.logout();

				this.router.navigate(['/authentication'])
					.then(() => {
						window.location.reload();
					});
			});
	}


	login(email: string, password: string): Observable<User | HttpErrorResponse> {

		return this.userService.authenticateUserByEmailAndPassword(email, password)
			.pipe(
				map(res => {
					if (res instanceof HttpErrorResponse) {
						this._user = null;
						this._state = res.error == 0 ? 'disconnected' : 'loggedOut';
						return res;
					}

					this._user = this.jwtToUser(res);
					if (this._user === null) throw new HttpErrorResponse({ error: 400 });

					localStorage.setItem(AuthenticationService.storageKey, res);
					this._state = 'loggedIn';
					return this._user;
				})
			);
	}

	register(email: string, password: string, firstName: string, lastName: string): Observable<User | HttpErrorResponse> {
		return this.userService.create(new UserCreateDto(email, password, firstName, lastName));
	}

	logout(): void {
		this._user = null;
		this._state = 'loggedOut';
		localStorage.removeItem(AuthenticationService.storageKey);

		this.router.navigate([''])
			.then(() => {
				window.location.reload();
			});
	}

	private jwtToUser(token: string): User | null {
		let decoded = jwtDecode<UserPayload>(token);
		if (
			decoded.sub === undefined ||
			decoded.email === undefined ||
			decoded.given_name === undefined ||
			decoded.family_name === undefined ||
			decoded.role === undefined ||
			decoded.exp === undefined ||
			decoded.exp <= Math.floor(Date.now() / 1000)
		) {
			return null;
		}

		return {
			id: parseInt(decoded.sub),
			email: decoded.email,
			role: UserRoles[decoded.role as keyof typeof UserRoles],
			firstName: decoded.given_name,
			lastName: decoded.family_name,
		}
	}
}


interface UserPayload extends JwtPayload {
	sub?: string,
	email?: string,
	given_name?: string,
	family_name?: string,
	role?: string
}