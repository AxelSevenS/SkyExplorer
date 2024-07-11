export interface IEntity {
	get id(): number;
}

export abstract class EntityCreateDto {
	abstract populate(formData: FormData): void;
}

export abstract class EntityUpdateDto {
	abstract populate(formData: FormData): void;
}