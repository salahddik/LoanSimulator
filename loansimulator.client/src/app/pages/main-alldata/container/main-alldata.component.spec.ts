import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { MainAlldataComponent } from './main-alldata.component';
import { ApiServiceService } from '../../../shared/services/api-service.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SharedModule } from '../../../shared/shared.module'; // Import your shared module
import { PagedResponse } from '../../../shared/interface/PagedResponse'; // Your actual interface
import { LoanSimResponsedata } from '../../../shared/interface/LoanSimResponsedata'; // Your actual interface

describe('MainAlldataComponent', () => {
  let component: MainAlldataComponent;
  let fixture: ComponentFixture<MainAlldataComponent>;
  let apiServiceSpy: jasmine.SpyObj<ApiServiceService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('ApiServiceService', ['getAllLoans']);

    await TestBed.configureTestingModule({
      declarations: [MainAlldataComponent],
      imports: [
        HttpClientTestingModule,
        SharedModule // Import the shared module containing the pipe
      ],
      providers: [
        { provide: ApiServiceService, useValue: spy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(MainAlldataComponent);
    component = fixture.componentInstance;
    apiServiceSpy = TestBed.inject(ApiServiceService) as jasmine.SpyObj<ApiServiceService>;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load loans on init and set loans & totalPages', () => {
    const mockResponse: PagedResponse<LoanSimResponsedata> = {
      items: [
        {
          amount: 5000,
          durationMonths: 12,
          interestRate: 3.5,
          monthlyPayment: 430,
          totalPayment: 5160,
          totalInterest: 160,
          email: 'user@example.com',
          message: 'Loan simulated successfully'
        }
      ],
      totalCount: 1,
      pageNumber: 1,
      pageSize: 10,
      totalPages: 5
    };

    apiServiceSpy.getAllLoans.and.returnValue(of(mockResponse));

    component.ngOnInit();
    fixture.detectChanges();

    expect(apiServiceSpy.getAllLoans).toHaveBeenCalledWith(component.pageNumber, component.pageSize);
    expect(component.loans).toEqual(mockResponse.items);
    expect(component.totalPages).toEqual(mockResponse.totalPages);
    expect(component.errorMessage).toBe('');
  });

  it('should set errorMessage and clear loans when loadLoans fails', () => {
    const error = { message: 'Failed to load' };
    apiServiceSpy.getAllLoans.and.returnValue(throwError(() => error));

    component.loadLoans();

    expect(component.errorMessage).toBe('Failed to load');
    expect(component.loans).toEqual([]);
  });

  it('should change page when onPageChange is called with valid page', () => {
    const mockResponse: PagedResponse<LoanSimResponsedata> = {
      items: [],
      totalCount: 0,
      pageNumber: 3,
      pageSize: 10,
      totalPages: 5
    };

    apiServiceSpy.getAllLoans.and.returnValue(of(mockResponse));

    component.totalPages = 5;
    component.onPageChange(3);

    expect(component.pageNumber).toBe(3);
    expect(apiServiceSpy.getAllLoans).toHaveBeenCalledWith(3, component.pageSize);
  });

  it('should ignore invalid page in onPageChange', () => {
    component.totalPages = 5;
    component.pageNumber = 1;

    apiServiceSpy.getAllLoans.calls.reset();

    component.onPageChange(0);
    expect(component.pageNumber).toBe(1);

    component.onPageChange(6);
    expect(component.pageNumber).toBe(1);

    expect(apiServiceSpy.getAllLoans).not.toHaveBeenCalled();
  });

  it('should change pageSize and reset pageNumber on onPageSizeChange', () => {
    const mockResponse: PagedResponse<LoanSimResponsedata> = {
      items: [],
      totalCount: 0,
      pageNumber: 1,
      pageSize: 20,
      totalPages: 5
    };

    apiServiceSpy.getAllLoans.and.returnValue(of(mockResponse));

    component.pageSize = 10;
    component.pageNumber = 2;

    component.onPageSizeChange(20);

    expect(component.pageSize).toBe(20);
    expect(component.pageNumber).toBe(1);
    expect(apiServiceSpy.getAllLoans).toHaveBeenCalledWith(1, 20);
  });
});
