import { Component, Input, OnInit, numberAttribute } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { EntityService } from '../../services/entity.service';
import { EntityCreateDto, EntityUpdateDto, IEntity } from '../../models/entity.model';


@Component({
	selector: 'se-entity-view',
	template: '',
	styles: [''],
})
export abstract class EntityViewComponent<T extends IEntity, TCreateDto extends EntityCreateDto, TUpdateDto extends EntityUpdateDto> implements OnInit {

	public get entity(): T | null | undefined {
		return this._entity;
	}
	@Input({alias: 'entity'}) public set entity(value: T | null) {
		this._entity = value;
		this.onUpdate();
	}
	private _entity?: T | null;


	public get id(): number | null | undefined {
		return this._entity?.id;
	}
	@Input({alias: 'entity-id', transform: numberAttribute}) public set id(value: number | null) {
		if (value === null) {
			this.update(undefined);
		}
		else {
			this.entityService.getById(value)
			.subscribe(entity => {
				if (entity instanceof HttpErrorResponse) return;

				this.update(entity);
			})
		}
	}


	constructor(
		public authentication: AuthenticationService,
		protected entityService: EntityService<T, TCreateDto, TUpdateDto>
	) { }

	ngOnInit() {

		this.entityService.eventRemoved
			.subscribe(entity => {
				if (this.entity?.id != entity.id) return;

				this.update(undefined);
			});

		this.entityService.eventUpdated
			.subscribe(entity => {
				if (this.entity?.id != entity.id) return;

				this.update(entity);
			});
	}

	async delete() {
		if( ! this.entity ) return;

		this.entityService.deleteById(this.entity.id)
			.subscribe(async res => {
				if (res instanceof HttpErrorResponse) {
					// const alert = await this.alertController.create({
					//   header: 'Erreur lors de la Suppression de l\'Utilisateur',
					//   message: 'La suppression de l\'Utilisateur a échoué',
					//   buttons: ['Ok'],
					// });

					// await alert.present();
					return;
				}
			});
	}

	public update(entity?: T) {
		this._entity = entity;
		this.onUpdate();
	}

	protected abstract onUpdate(): void;
}