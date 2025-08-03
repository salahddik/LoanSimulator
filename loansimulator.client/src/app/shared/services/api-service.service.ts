import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { LoanCreditSimRequest } from '../interface/loan-credit-sim-request';
import { catchError, Observable, throwError } from 'rxjs';
import { LoanCreditSimResponse } from '../interface/loan-credit-sim-response';
import { LoanSimResponsedata } from '../interface/LoanSimResponsedata';
import { PagedResponse } from '../interface/PagedResponse';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {
  private readonly baseUrl = environment.apiBaseUrl;
  private readonly loanCreditSimUrl = this.baseUrl + environment.apiEndpoints.loans;
  private readonly getAllLoansUrl = this.baseUrl + environment.apiEndpoints.GETALLDATA;

  constructor(private http: HttpClient) { }

  postLoanCreditSim(payload: LoanCreditSimRequest): Observable<LoanCreditSimResponse> {
    return this.http.post<LoanCreditSimResponse>(this.loanCreditSimUrl, payload)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          return throwError(() => error);
        })
      );
  }

  getAllLoans(pageNumber: number, pageSize: number): Observable<PagedResponse<LoanSimResponsedata>> {
    const url = `${this.getAllLoansUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}`;
    return this.http.get<PagedResponse<LoanSimResponsedata>>(url)
      .pipe(catchError(this.handleError));
  }

  private handleError = (error: HttpErrorResponse) => {
    let errorMessage = '';

    if (error.error instanceof ErrorEvent) {
      errorMessage = `Client-side error: ${error.error.message}`;
    } else {
      errorMessage = `Server returned code ${error.status}, message: ${error.message}`;
    }

    // Removed console.error here
    return throwError(() => new Error(errorMessage));
  }
}
