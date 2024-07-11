import { IEntity, EntityCreateDto, EntityUpdateDto } from "../../core/models/entity.model";

export interface Plane extends IEntity {
	id: number,
	name: string;
	type: string;
	status: PlaneStatus;
}

export class PlaneCreateDto extends EntityCreateDto {
	name: string;
	type: string;
	status: PlaneStatus;

	constructor(
		name: string,
		type: string,
		status: PlaneStatus,
	) {
		super();
		this.name = name;
		this.type = type;
		this.status = status;
	}

	override populate(formData: FormData): void {
		formData.append('Name', this.name);
		formData.append('Type', this.type);
		formData.append('Status', PlaneStatus[this.status]);
	}
}

export class PlaneUpdateDto extends EntityUpdateDto {
	status?: PlaneStatus;

	constructor(
		{ status }:
		{ status?: PlaneStatus } =
		{ }
	) {
		super();
		this.status = status;
	}

	override populate(formData: FormData): void {
		if (this.status !== undefined) formData.append('Status', PlaneStatus[this.status]);
	}
}

export enum PlaneStatus {
	Available = 0,
	Maintenance = 1,
	Unavailable = 2,
}