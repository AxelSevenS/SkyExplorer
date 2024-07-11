import { EntityCreateDto, EntityUpdateDto, IEntity } from "../../core/models/entity.model";
import { User } from "../../users/models/user.model";

export interface Bill extends IEntity {
	id: number,
	user: User,

	name: string,
	url: string,
	wasAcquitted: boolean,

	createdAt: Date,
}

export class BillCreateDto extends EntityCreateDto {
	userId: number;
	url: string;
	name: string;
	wasAcquitted: boolean;

	constructor(
		userId: number,
		url: string,
		name: string,
		wasAcquitted: boolean,
	) {
		super();
		this.userId = userId;
		this.url = url;
		this.name = name;
		this.wasAcquitted = wasAcquitted;
	}

	override populate(formData: FormData): void {
		formData.append('UserId', this.userId.toString());
		formData.append('URL', this.url);
		formData.append('Name', this.name);
		formData.append('WasAcquitted', this.wasAcquitted ? "true" : "false");

		console.log(formData.get('UserId'));
		console.log(formData.get('URL'));
		console.log(formData.get('Name'));
		console.log(formData.get('WasAcquitted'));
	}
}

export class BillUpdateDto extends EntityUpdateDto {
	userId?: number;
	url?: string;
	name?: string;
	wasAcquitted?: boolean;

	constructor(
		{ userId, url, name, wasAcquitted }:
		{ userId?: number, url?: string, name?: string, wasAcquitted?: boolean } =
		{ }
	) {
		super();
		this.userId = userId;
		this.url = url;
		this.name = name;
		this.wasAcquitted = wasAcquitted;
	}

	override populate(formData: FormData): void {
		if (this.userId !== undefined) formData.append('UserId', this.userId.toString());
		if (this.url !== undefined) formData.append('URL', this.url);
		if (this.name !== undefined) formData.append('Name', this.name);
		if (this.wasAcquitted !== undefined) formData.append('WasAcquitted', this.wasAcquitted ? "true" : "false");
	}
}