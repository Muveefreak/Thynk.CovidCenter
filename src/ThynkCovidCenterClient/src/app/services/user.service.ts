import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment.prod';
import { SubmitCreateRequestPayload } from '../models/user.models';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  apiURL = environment.baseURI;
// test$: Observable<any> = this._httpClient.get<any>('/api/pendingRequests');
constructor(private _httpClient: HttpClient) { }

CreateUser(payload: SubmitCreateRequestPayload): Observable<any> {
  return this._httpClient.post<any>(this.apiURL + 'User/create-user', payload)
    .pipe(retry(3), catchError(this.handleError));
}

// Error handling
handleError(error: any) {
  console.log('error :>> ', error);
  let errorMessage = 'Something went wrong, please try again';
  return throwError(errorMessage);
}

}
