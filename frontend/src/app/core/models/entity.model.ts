export abstract class EntityCreateDto {
	abstract populate(formData: FormData): void;
}

export abstract class EntityUpdateDto {
	abstract populate(formData: FormData): void;
}