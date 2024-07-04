export interface User {
	id: number,
	email: string,
	firstName: string,
	lastName: string,
	role: UserRoles,
}

export enum UserRoles {
	User = "User",
	Collaborator = "Collaborator",
	Staff = "Staff",
	Admin = "Admin",
}