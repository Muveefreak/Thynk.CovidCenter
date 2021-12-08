import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment.prod';
import { SubmitAvailDateRequestPayload } from '../models/availDate.model';
import { BookingStatus, SubmitCancelBookingRequestPayload, SubmitCreateBookingRequestPayload } from '../models/booking.model';
import { SubmitCreateLocationRequestPayload } from '../models/location.models';


@Injectable({
  providedIn: 'root'
})
export class BookingService {
  apiURL = environment.baseURI;
constructor(private _httpClient: HttpClient) { }

CreateBooking(payload: SubmitCreateBookingRequestPayload): Observable<any> {
    return this._httpClient.post<any>(this.apiURL + 'Booking/create-booking', payload)
      .pipe(retry(3), catchError(this.handleError));
  }

GetBooking(bookingStatus: BookingStatus): Observable<any> {
  return this._httpClient.post<any>(this.apiURL + 'Booking/get-booking', bookingStatus)
    .pipe(retry(3), catchError(this.handleError));
}

CancelBooking(payload: SubmitCancelBookingRequestPayload): Observable<any> {
  return this._httpClient.post<any>(this.apiURL + 'Booking/cancel-booking', payload)
    .pipe(retry(3), catchError(this.handleError));
}

// Error handling
handleError(error: any) {
  console.log('error :>> ', error);
  let errorMessage = 'Something went wrong, please try again';
  return throwError(errorMessage);
}

}
