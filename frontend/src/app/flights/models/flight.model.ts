import moment from "moment";
import { Bill } from "../../bills/models/bill.model";
import { IEntity, EntityCreateDto, EntityUpdateDto } from "../../core/models/entity.model";
import { Plane } from "../../planes/models/plane.model";
import { User } from "../../users/models/user.model";

export interface Flight extends IEntity {
	id: number,

	user: User,
	overseer: User,
	bill: Bill,
	plane: Plane,

	duration: string,
	dateTime: string,
}

export class FlightCreateDto extends EntityCreateDto {
	userId: number;
	overseerId: number;
	billId: number;
	planeId: number;

	duration: string;
	dateTime: string;

	constructor(
		userId: number,
		overseerId: number,
		billId: number,
		planeId: number,

		duration: string,
		dateTime: string,
	) {
		super();
		this.userId = userId;
		this.overseerId = overseerId;
		this.billId = billId;
		this.planeId = planeId;

		this.duration = duration;
		this.dateTime = dateTime;
	}

	override populate(formData: FormData): void {
		formData.append('UserId', this.userId.toString());
		console.log('UserId', formData.get('UserId'));
		formData.append('OverseerId', this.overseerId.toString());
		console.log('OverseerId', formData.get('OverseerId'));
		formData.append('BillId', this.billId.toString());
		console.log('BillId', formData.get('BillId'));
		formData.append('PlaneId', this.planeId.toString());
		console.log('PlaneId', formData.get('PlaneId'));

		formData.append('Duration', this.duration);
		console.log('Duration', formData.get('Duration'));
		formData.append('DateTime', this.dateTime);
		console.log('DateTime', formData.get('DateTime'));
		// formData.append('Duration', "02:00:00");
		// formData.append('DateTime', "2024-07-09T10:00:00Z");
	}
}

export class FlightUpdateDto extends EntityUpdateDto {
	overseerId?: number;
	billId?: number;
	planeId?: number;

	duration?: string;
	dateTime?: string;

	constructor(
		{ overseerId, billId, planeId, duration, dateTime }:
		{ overseerId?: number, billId?: number, planeId?: number, duration?: string, dateTime?: string } =
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
		if (this.overseerId !== undefined) formData.append('OverseerId', this.overseerId.toString());
		if (this.billId !== undefined) formData.append('BillId', this.billId.toString());
		if (this.planeId !== undefined) formData.append('PlaneId', this.planeId.toString());

		if (this.duration !== undefined) formData.append('Duration', this.duration);
		if (this.dateTime !== undefined) formData.append('DateTime', this.dateTime);
	}
}