import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment.prod';
import { SubmitCreateLocationRequestPayload } from '../models/location.models';


@Injectable({
  providedIn: 'root'
})
export class LocationService {
  apiURL = environment.baseURI;
constructor(private _httpClient: HttpClient) { }

CreateLocation(payload: SubmitCreateLocationRequestPayload): Observable<any> {
  return this._httpClient.post<any>(this.apiURL + 'Location/create-location', payload)
    .pipe(retry(3), catchError(this.handleError));
}


GetLocations(): Observable<any> {
  return this._httpClient.post<any>(this.apiURL + 'Location/get-locations', null)
    .pipe(retry(3), catchError(this.handleError));
}

// Error handling
handleError(error: any) {
  console.log('error :>> ', error);
  let errorMessage = 'Something went wrong, please try again';
  return throwError(errorMessage);
}

}
