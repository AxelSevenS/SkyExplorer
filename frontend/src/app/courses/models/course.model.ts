import { EntityCreateDto, EntityUpdateDto } from "../../core/models/entity.model";
import { Flight } from "../../flights/models/flight.model";

export interface Course {
	id: number,
	flight: Flight,
	goals: string,
	achievedGoals: string,
	notes: string,
}

export class CourseCreateDto extends EntityCreateDto {
	flightId: number;
	goals?: string;
	achievedGoals?: string;
	notes?: string;

	constructor(
		flightId: number,
		goals?: string,
		achievedGoals?: string,
		notes?: string,
	) {
		super();
		this.flightId = flightId;
		this.goals = goals;
		this.achievedGoals = achievedGoals;
		this.notes = notes;
	}

	override populate(formData: FormData): void {
		formData.append('FlightId', this.flightId.toString());
		if (this.goals) formData.append('Goals', this.goals);
		if (this.achievedGoals) formData.append('AchievedGoals', this.achievedGoals);
		if (this.notes) formData.append('Notes', this.notes);
	}
}

export class CourseUpdateDto extends EntityUpdateDto {
	goals?: string;
	achievedGoals?: string;
	notes?: string;

	constructor(
		{ goals, achievedGoals, notes }:
		{ goals?: string, achievedGoals?: string, notes?: string } =
		{ }
	) {
		super();
		this.goals = goals;
		this.achievedGoals = achievedGoals;
		this.notes = notes;
	}

	override populate(formData: FormData): void {
		if (this?.goals) formData.append('Goals', this.goals);
		if (this?.achievedGoals) formData.append('AchievedGoals', this.achievedGoals);
		if (this?.notes) formData.append('Notes', this.notes);
	}
}