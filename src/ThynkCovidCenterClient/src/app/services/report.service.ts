import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment.prod';
import { SubmitAvailDateRequestPayload } from '../models/availDate.model';
import { BookingStatus, SubmitCancelBookingRequestPayload, SubmitCreateBookingRequestPayload } from '../models/booking.model';
import { SubmitCreateLocationRequestPayload } from '../models/location.models';
import { SubmitEnterResultRequestPayload, SubmitGetReportRequestPayload } from '../models/report.model';


@Injectable({
  providedIn: 'root'
})
export class ReportService {
  apiURL = environment.baseURI;
constructor(private _httpClient: HttpClient) { }

EnterResultBooking(payload: SubmitEnterResultRequestPayload): Observable<any> {
    return this._httpClient.post<any>(this.apiURL + 'Report/booking-result', payload)
      .pipe(retry(3), catchError(this.handleError));
  }

GetReport(payload: SubmitGetReportRequestPayload): Observable<any> {
  return this._httpClient.post<any>(this.apiURL + 'Report/get-results', payload)
    .pipe(retry(3), catchError(this.handleError));
}

// Error handling
handleError(error: any) {
  console.log('error :>> ', error);
  let errorMessage = 'Something went wrong, please try again';
  return throwError(errorMessage);
}

}
