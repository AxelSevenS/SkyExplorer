import { IEntity, EntityCreateDto, EntityUpdateDto } from "../../core/models/entity.model";

export interface User extends IEntity {
	id: number,
	email: string,
	firstName: string,
	lastName: string,
	role: UserRoles,
}

export class UserCreateDto extends EntityCreateDto {
	email: string;
	password: string;
	firstName: string;
	lastName: string;

	constructor(
		email: string,
		password: string,
		firstName: string,
		lastName: string,
	) {
		super();
		this.email = email;
		this.password = password;
		this.firstName = firstName;
		this.lastName = lastName;
	}

	override populate(formData: FormData): void {
		formData.append('Email', this.email);
		formData.append('Password', this.password);
		formData.append('FirstName', this.firstName);
		formData.append('LastName', this.lastName);
	}
}

export class UserUpdateDto extends EntityUpdateDto {
	email?: string;
	password?: string;
	firstName?: string;
	lastName?: string;
	role?: UserRoles;

	constructor(
		{ email, password, firstName, lastName, role }:
		{ email?: string, password?: string, firstName?: string, lastName?: string, role?: UserRoles, } =
		{ }
	) {
		super();
		this.email = email;
		this.password = password;
		this.firstName = firstName;
		this.lastName = lastName;
		this.role = role;
	}

	override populate(formData: FormData): void {
		if (this.email !== undefined) formData.append('Email', this.email);
		if (this.password !== undefined) formData.append('Password', this.password);
		if (this.firstName !== undefined) formData.append('FirstName', this.firstName);
		if (this.lastName !== undefined) formData.append('LastName', this.lastName);
		if (this.role !== undefined) formData.append('Role', UserRoles[this.role]);
	}
}

export enum UserRoles {
	User = 0,
	Collaborator = 1,
	Staff = 2,
	Admin = 3,
}