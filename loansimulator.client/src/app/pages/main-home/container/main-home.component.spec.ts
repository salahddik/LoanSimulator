import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { MainHomeComponent } from './main-home.component';
import { ApiServiceService } from '../../../shared/services/api-service.service';
import { LoanCreditSimResponse } from '../../../shared/interface/loan-credit-sim-response';
import { FormsModule } from '@angular/forms';

class MockApiService {
  postLoanCreditSim = jasmine.createSpy('postLoanCreditSim');
}

describe('MainHomeComponent', () => {
  let component: MainHomeComponent;
  let fixture: ComponentFixture<MainHomeComponent>;
  let apiService: MockApiService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MainHomeComponent],
      imports: [FormsModule],
      providers: [{ provide: ApiServiceService, useClass: MockApiService }]
    }).compileComponents();

    fixture = TestBed.createComponent(MainHomeComponent);
    component = fixture.componentInstance;
    apiService = TestBed.inject(ApiServiceService) as unknown as MockApiService;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call API and set loanResponse on success', () => {
    const mockResponse: LoanCreditSimResponse = {
      amount: 1000,
      durationMonths: 12,
      interestRate: 5,
      monthlyPayment: 100,
      totalPayment: 1200,
      totalInterest: 200,
      email: 'test@example.com',
      message: 'Simulation successful'
    };

    apiService.postLoanCreditSim.and.returnValue(of(mockResponse));

    component.simulateLoan();

    expect(apiService.postLoanCreditSim).toHaveBeenCalledWith(component.loanRequest);
    expect(component.loanResponse).toEqual(mockResponse);
    expect(component.isLoading).toBeFalse();
    expect(component.errorMessage).toBeUndefined();
  });

  it('should handle validation errors from backend', () => {
    const backendError = {
      error: { errors: { amount: ['Amount is required'], email: ['Invalid email'] } }
    };

    apiService.postLoanCreditSim.and.returnValue(throwError(() => backendError));

    component.simulateLoan();

    expect(component.errorMessage).toContain('Amount is required');
    expect(component.errorMessage).toContain('Invalid email');
    expect(component.loanResponse).toBeUndefined();
    expect(component.isLoading).toBeFalse();
  });

  it('should handle other backend message errors', () => {
    const backendError = {
      error: { message: 'Something went wrong' }
    };

    apiService.postLoanCreditSim.and.returnValue(throwError(() => backendError));

    component.simulateLoan();

    expect(component.errorMessage).toBe('Something went wrong');
    expect(component.loanResponse).toBeUndefined();
    expect(component.isLoading).toBeFalse();
  });

  it('should handle unknown errors', () => {
    const backendError = {};

    apiService.postLoanCreditSim.and.returnValue(throwError(() => backendError));

    component.simulateLoan();

    expect(component.errorMessage).toBe('Failed to simulate loan. Please try again later.');
    expect(component.loanResponse).toBeUndefined();
    expect(component.isLoading).toBeFalse();
  });
});
