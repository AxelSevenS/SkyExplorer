import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Flight, FlightCreateDto, FlightUpdateDto } from '../models/flight.model';
import { Observable, catchError, of, share } from 'rxjs';
import { EntityService } from '../../core/services/entity.service';

@Injectable({
	providedIn: 'root'
})
export class FlightService extends EntityService<Flight, FlightCreateDto, FlightUpdateDto> {

	protected override get endpointSuffix(): string { return 'flights' }


	constructor(
		protected override http: HttpClient
	) {
		super(http);
	}
}