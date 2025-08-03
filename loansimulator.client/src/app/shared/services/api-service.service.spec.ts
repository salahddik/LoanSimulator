import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ApiServiceService } from './api-service.service';
import { LoanCreditSimRequest } from '../interface/loan-credit-sim-request';
import { LoanCreditSimResponse } from '../interface/loan-credit-sim-response';
import { LoanSimResponsedata } from '../interface/LoanSimResponsedata';
import { PagedResponse } from '../interface/PagedResponse';
import { environment } from '../../../environments/environment';
import { HttpErrorResponse } from '@angular/common/http';

describe('ApiServiceService', () => {
  let service: ApiServiceService;
  let httpMock: HttpTestingController;
  const baseUrl = environment.apiBaseUrl;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ApiServiceService]
    });
    service = TestBed.inject(ApiServiceService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Verify no outstanding requests
  });

  describe('postLoanCreditSim', () => {
    const mockRequest: LoanCreditSimRequest = {
      amount: 10000,
      durationMonths: 12,
      email: 'test@example.com'
    };

    const mockResponse: LoanCreditSimResponse = {
      amount: 10000,
      durationMonths: 12,
      interestRate: 3.5,
      monthlyPayment: 856.07,
      totalPayment: 10272.84,
      totalInterest: 272.84,
      email: 'test@example.com',
      message: 'Simulation successful'
    };

    it('should make POST request with correct URL and data', () => {
      service.postLoanCreditSim(mockRequest).subscribe(response => {
        expect(response).toEqual(mockResponse);
      });

      const req = httpMock.expectOne(`${baseUrl}${environment.apiEndpoints.loans}`);
      expect(req.request.method).toBe('POST');
      expect(req.request.body).toEqual(mockRequest);
      req.flush(mockResponse);
    });

    it('should handle 400 error for invalid loan amount', () => {
      const errorResponse = {
        status: 400,
        statusText: 'Bad Request',
        error: { message: 'Amount must be positive' } // or string
      };

      service.postLoanCreditSim(mockRequest).subscribe({
        next: () => fail('should have failed with 400 error'),
        error: (error) => {
          expect(error.status).toBe(400);
          // Handle both string and object error responses
          const errorMessage = typeof error.error === 'string'
            ? error.error
            : error.error?.message;
          expect(errorMessage).toBe('Amount must be positive');
        }
      });

      const req = httpMock.expectOne(`${baseUrl}${environment.apiEndpoints.loans}`);
      req.flush('Amount must be positive', errorResponse);
    });
  });

  describe('getAllLoans', () => {
    const mockResponse: PagedResponse<LoanSimResponsedata> = {
      items: [{
        amount: 10000,
        durationMonths: 12,
        interestRate: 3.5,
        monthlyPayment: 856.07,
        totalPayment: 10272.84,
        totalInterest: 272.84,
        email: 'test@example.com',
        message: 'Simulation successful'
      }],
      totalCount: 1,
      pageNumber: 1,
      pageSize: 10,
      totalPages: 1
    };

    it('should include pagination parameters in GET request', () => {
      const pageNumber = 2;
      const pageSize = 5;

      service.getAllLoans(pageNumber, pageSize).subscribe(response => {
        expect(response.items.length).toBe(1);
        expect(response.totalCount).toBe(1);
      });

      const req = httpMock.expectOne(
        `${baseUrl}${environment.apiEndpoints.GETALLDATA}?pageNumber=2&pageSize=5`
      );
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
    });

    it('should handle empty response', () => {
      const emptyResponse: PagedResponse<LoanSimResponsedata> = {
        items: [],
        totalCount: 0,
        pageNumber: 1,
        pageSize: 10,
        totalPages: 0
      };

      service.getAllLoans(1, 10).subscribe(response => {
        expect(response.items.length).toBe(0);
        expect(response.totalCount).toBe(0);
      });

      const req = httpMock.expectOne(
        `${baseUrl}${environment.apiEndpoints.GETALLDATA}?pageNumber=1&pageSize=10`
      );
      req.flush(emptyResponse);
    });
  });

  describe('error handling', () => {
    it('should transform error response for invalid email', () => {
      const mockRequest: LoanCreditSimRequest = {
        amount: 10000,
        durationMonths: 12,
        email: 'invalid-email'
      };

      service.postLoanCreditSim(mockRequest).subscribe({
        next: () => fail('should have failed with 422 error'),
        error: (error: HttpErrorResponse) => {
          // Check the status code directly
          expect(error.status).toBe(422);
          // Check the error message format
          expect(error.message).toContain('Http failure response');
          expect(error.message).toContain('422');
          // Check the actual error content if available
          if (typeof error.error === 'string') {
            expect(error.error).toContain('Invalid email format');
          } else if (error.error && typeof error.error === 'object') {
            expect(error.error.message).toContain('Invalid email format');
          }
        }
      });

      const req = httpMock.expectOne(`${baseUrl}${environment.apiEndpoints.loans}`);
      req.flush('Invalid email format', {
        status: 422,
        statusText: 'Unprocessable Entity'
      });
    });
  });
});
