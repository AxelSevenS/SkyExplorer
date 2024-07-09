import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Bill, BillCreateDto, BillUpdateDto } from '../models/bill.model';
import { Observable, catchError, of, share } from 'rxjs';
import { EntityService } from '../../core/services/entity.service';

@Injectable({
	providedIn: 'root'
})
export class BillService extends EntityService<Bill, BillCreateDto, BillUpdateDto> {

	protected override get endpointSuffix(): string { return 'bills' }


	constructor(
		protected override http: HttpClient
	) {
		super(http);
	}


}