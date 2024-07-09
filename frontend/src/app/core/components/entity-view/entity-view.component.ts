import { Component, Input, OnInit, numberAttribute } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthenticationService } from '../../../authentication/services/authentication.service';
import { EntityService } from '../../services/entity.service';
import { EntitySetupDto, EntityUpdateDto, IEntity } from '../../models/entity.model';


@Component({
	selector: 'se-entity-view',
	templateUrl: './entity-view.component.html',
	styleUrls: ['./entity-view.component.scss'],
})
export abstract class EntityViewComponent<T extends IEntity, TSetupDto extends EntitySetupDto, TUpdateDto extends EntityUpdateDto> implements OnInit {

	@Input({alias: 'entity'}) public entity?: T | null;
	@Input({alias: 'entity-id', transform: numberAttribute}) public id?: number;

	constructor(
		public authentication: AuthenticationService,
		protected entityService: EntityService<T, TSetupDto, TUpdateDto>,
	) { }

	ngOnInit() {
		if (this.id && ! this.entity) {
			this.entityService.getById(this.id)
				.subscribe(course => {
					if (course instanceof HttpErrorResponse) return;

					this.entity = course;
				})
		} else if (! this.id && this.entity) {
			this.id = this.entity.id;
		}

		this.entityService.eventRemoved
			.subscribe(course => {
				if (this.entity?.id != course.id) return;
				this.entity = null;
			});

		this.entityService.eventUpdated
			.subscribe(course => {
				if (this.entity?.id != course.id) return;
				this.entity = course;
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
}