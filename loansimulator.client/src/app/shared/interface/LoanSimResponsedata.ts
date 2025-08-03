export interface LoanSimResponsedata {
  amount: number;
  durationMonths: number;
  interestRate: number;
  monthlyPayment: number;
  totalPayment: number;
  totalInterest: number;
  email: string;
  message: string;
}
