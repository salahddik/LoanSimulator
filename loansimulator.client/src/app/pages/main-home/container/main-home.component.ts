import { Component } from '@angular/core';
import { LoanCreditSimRequest } from '../../../shared/interface/loan-credit-sim-request';
import { LoanCreditSimResponse } from '../../../shared/interface/loan-credit-sim-response';
import { ApiServiceService } from '../../../shared/services/api-service.service';


@Component({
  selector: 'app-main-home',
  standalone: false,
  templateUrl: './main-home.component.html',
  styleUrls: ['./main-home.component.css']
})
export class MainHomeComponent {
  loanRequest: LoanCreditSimRequest = {
    amount: 0,
    durationMonths: 0,
    email: ''
  };

  loanResponse?: LoanCreditSimResponse;
  errorMessage?: string;
  isLoading = false;

  constructor(private apiService: ApiServiceService) { }

  simulateLoan() {
    this.isLoading = true;
    this.errorMessage = undefined;

    this.apiService.postLoanCreditSim(this.loanRequest).subscribe({
      next: (response: LoanCreditSimResponse) => {
        this.loanResponse = response;
        this.isLoading = false;
      },
      error: (error: any) => {
        console.error('Full error object from backend:', error);

        const validationErrors = error.error?.errors;

        if (validationErrors) {
          const allErrors = Object.values(validationErrors)
            .flat()
            .join('; ');

          this.errorMessage = allErrors;
          console.error('Backend validation errors:', allErrors);
        } else if (error.error?.message) {
          this.errorMessage = error.error.message;
          console.error('Other error:', error.error.message);
        } else if (error.error) {
          // Show full raw error JSON string if no message or errors found
          this.errorMessage = JSON.stringify(error.error);
          console.error('Raw error object as string:', this.errorMessage);
        } else {
          this.errorMessage = 'Failed to simulate loan. Please try again later.';
        }

        this.loanResponse = undefined;
        this.isLoading = false;
      }

    });
  }

}
