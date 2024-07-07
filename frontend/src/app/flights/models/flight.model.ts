import { EntityCreateDto, EntityUpdateDto } from "../../core/models/entity.model";
import { Plane } from "../../planes/models/plane.model";

export interface Flight {
	id: number,
	userId: number,
	overseerId: number,
	billId: number,
	plane: Plane,
	flightType: string,
	duration: Date,
	dateTime: Date,
}

export class FlightCreateDto extends EntityCreateDto {
	userId: number;
	overseerId: number;
	billId: number;
	plane: Plane;
	flightType: string;
	duration: Date;
	dateTime: Date;

	constructor(
		userId: number,
		overseerId: number,
		billId: number,
		plane: Plane,
		flightType: string,
		duration: Date,
		dateTime: Date,
	) {
		super();
		this.userId = userId;
		this.overseerId = overseerId;
		this.billId = billId;
		this.plane = plane;
		this.flightType = flightType;
		this.duration = duration;
		this.dateTime = dateTime;
	}

	override populate(formData: FormData): void {
		formData.append('UserId', this.userId.toString());
		formData.append('OverseerId', this.overseerId.toString());
		formData.append('BillId', this.billId.toString());
		formData.append('FlightType', this.flightType);
		formData.append('Duration', this.duration.toTimeString());
		formData.append('DateTime', this.dateTime.toString());
	}
}

export class FlightUpdateDto extends EntityUpdateDto {
	overseerId?: number;
	billId?: number;
	planeId?: number;
	duration?: Date;
	dateTime?: Date;

	constructor(
		{ overseerId, billId, planeId, duration, dateTime }:
		{ overseerId?: number, billId?: number, planeId?: number, duration?: Date, dateTime?: Date } =
		{ }
	) {
		super();
		this.overseerId = overseerId;
		this.billId = billId;
		this.planeId = planeId;
		this.duration = duration;
		this.dateTime = dateTime;
	}

	override populate(formData: FormData): void {
		if (this?.overseerId) formData.append('OverseerId', this.overseerId.toString());
		if (this?.billId) formData.append('BillId', this.billId.toString());
		if (this?.planeId) formData.append('PlaneId', this.planeId.toString());
		if (this?.duration) formData.append('Duration', this.duration.toTimeString());
		if (this?.dateTime) formData.append('DateTime', this.dateTime.toDateString());
	}
}

// export enum FlightType {
// 	Course = 0,
// 	Leasure = 1,
// }