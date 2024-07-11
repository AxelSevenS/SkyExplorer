import { EntitySetupDto, EntityUpdateDto, IEntity } from "../../core/models/entity.model";

export interface Bill extends IEntity {
	id: number,

	name: string,
	url: string,
	wasAcquitted: boolean,

	createdAt: Date,
}

export class BillCreateDto extends EntitySetupDto {
	url: string;
	name: string;
	wasAcquitted: boolean;

	constructor(
		url: string,
		name: string,
		wasAcquitted: boolean,
	) {
		super();
		this.url = url;
		this.name = name;
		this.wasAcquitted = wasAcquitted;
	}

	override populate(formData: FormData): void {
		formData.append('URL', this.url);
		formData.append('Name', this.name);
		formData.append('WasAcquitted', this.wasAcquitted ? "true" : "false");
	}
}

export class BillUpdateDto extends EntityUpdateDto {
	url?: string;
	name?: string;
	wasAcquitted?: boolean;

	constructor(
		{ url, name, wasAcquitted }:
		{ url?: string, name?: string, wasAcquitted?: boolean } =
		{ }
	) {
		super();
		this.url = url;
		this.name = name;
		this.wasAcquitted = wasAcquitted;
	}

	override populate(formData: FormData): void {
		if (this.url !== undefined) formData.append('URL', this.url);
		if (this.name !== undefined) formData.append('Name', this.name);
		if (this.wasAcquitted !== undefined) formData.append('WasAcquitted', this.wasAcquitted ? "true" : "false");
	}
}