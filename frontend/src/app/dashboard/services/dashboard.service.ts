import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, of, share } from 'rxjs';
import { EntityService } from '../../core/services/entity.service';
import { Flight, FlightCreateDto, FlightUpdateDto } from '../../flights/models/flight.model';

@Injectable({
	providedIn: 'root'
})
export class dashBoardService extends EntityService<Flight, FlightCreateDto, FlightUpdateDto> {

	protected override get endpointSuffix(): string { return 'courses' }


	constructor(
		protected override http: HttpClient
	) {
		super(http);
	}

}