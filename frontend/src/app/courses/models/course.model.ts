import { IEntity, EntityCreateDto, EntityUpdateDto } from "../../core/models/entity.model";
import { Flight } from "../../flights/models/flight.model";

export interface Course extends IEntity {
	id: number,
	name: string;
	flight: Flight,
	goals: string,
	achievedGoals: string,
	notes: string,
	acquiredSkills: string,
}

export class CourseCreateDto extends EntityCreateDto {
	name: string;
	flightId: number;
	goals?: string;
	achievedGoals?: string;
	notes?: string;
	acquiredSkills?: string;

	constructor(
		name: string,
		flightId: number,
		goals?: string,
		achievedGoals?: string,
		notes?: string,
		acquiredSkills?: string
	) {
		super();
		this.name = name;
		this.flightId = flightId;
		this.goals = goals;
		this.achievedGoals = achievedGoals;
		this.notes = notes;
		this.acquiredSkills = acquiredSkills;
	}

	override populate(formData: FormData): void {
		formData.append('Name', this.name);
		formData.append('FlightId', this.flightId.toString());
		if (this.goals) formData.append('Goals', this.goals);
		if (this.achievedGoals) formData.append('AchievedGoals', this.achievedGoals);
		if (this.notes) formData.append('Notes', this.notes);
		if (this.acquiredSkills) formData.append('AcquiredSkills', this.acquiredSkills);
	}
}

export class CourseUpdateDto extends EntityUpdateDto {
	name?: string;
	goals?: string;
	achievedGoals?: string;
	notes?: string;
	acquiredSkills?: string;

	constructor(
		{ name, goals, achievedGoals, notes ,acquiredSkills }:
		{ name?: string, goals?: string, achievedGoals?: string, notes?: string, acquiredSkills?:string} =
		{ }
	) {
		super();
		this.name = name;
		this.goals = goals;
		this.achievedGoals = achievedGoals;
		this.notes = notes;
		this.acquiredSkills = acquiredSkills;
	}

	override populate(formData: FormData): void {
		if (this.name !== undefined) formData.append('Name', this.name);
		if (this.goals !== undefined) formData.append('Goals', this.goals);
		if (this.achievedGoals !== undefined) formData.append('AchievedGoals', this.achievedGoals);
		if (this.notes !== undefined) formData.append('Notes', this.notes);
		if (this.acquiredSkills !== undefined) formData.append('AcquiredSkills', this.acquiredSkills);
	}
}