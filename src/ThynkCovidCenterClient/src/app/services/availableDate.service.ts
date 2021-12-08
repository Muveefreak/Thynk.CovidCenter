import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment.prod';
import { SubmitAvailDateRequestPayload } from '../models/availDate.model';
import { SubmitCreateLocationRequestPayload } from '../models/location.models';


@Injectable({
  providedIn: 'root'
})
export class AvailableDateService {
  apiURL = environment.baseURI;
constructor(private _httpClient: HttpClient) { }

CreateAvailDates(payload: SubmitAvailDateRequestPayload): Observable<any> {
    return this._httpClient.post<any>(this.apiURL + 'AvailableDates/create-available-dates', payload)
      .pipe(retry(3), catchError(this.handleError));
  }

GetAvailDates(locationId: string): Observable<any> {
  return this._httpClient.post<any>(this.apiURL + 'AvailableDates/get-available-dates-by-location?locationId='+locationId, null)
    .pipe(retry(3), catchError(this.handleError));
}

// Error handling
handleError(error: any) {
  console.log('error :>> ', error);
  let errorMessage = 'Something went wrong, please try again';
  return throwError(errorMessage);
}

}
