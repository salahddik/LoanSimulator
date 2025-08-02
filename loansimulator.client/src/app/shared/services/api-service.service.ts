import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { LoanCreditSimRequest } from '../interface/loan-credit-sim-request';
import { catchError, Observable, throwError } from 'rxjs';
import { LoanCreditSimResponse } from '../interface/loan-credit-sim-response';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {
  private readonly baseUrl = environment.apiBaseUrl;
  private readonly loanCreditSimUrl = this.baseUrl + environment.apiEndpoints.loans;

  constructor(private http: HttpClient) { }

  postLoanCreditSim(payload: LoanCreditSimRequest): Observable<LoanCreditSimResponse> {
    return this.http.post<LoanCreditSimResponse>(this.loanCreditSimUrl, payload)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          // Pass the complete error response to the component
          return throwError(() => error);
        })
      );
  }
}
