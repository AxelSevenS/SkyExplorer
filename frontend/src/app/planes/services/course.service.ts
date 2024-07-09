import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Plane, PlaneCreateDto, PlaneUpdateDto } from '../models/plane.model';
import { Observable, catchError, of, share } from 'rxjs';
import { EntityService } from '../../core/services/entity.service';

@Injectable({
	providedIn: 'root'
})
export class PlaneService extends EntityService<Plane, PlaneCreateDto, PlaneUpdateDto> {

	protected override get endpointSuffix(): string { return 'planes' }


	constructor(
		protected override http: HttpClient
	) {
		super(http);
	}
}