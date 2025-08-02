using System;

namespace LoanSimulator.Application.CORS.Queries
{
    public class LoanSimulationResultDto
    {
        public decimal Amount { get; set; }
        public int DurationMonths { get; set; }
        public double InterestRate { get; set; }

        private decimal monthlyPayment;
        public decimal MonthlyPayment
        {
            get => Math.Round(monthlyPayment, 2);
            set => monthlyPayment = value;
        }

        private decimal totalPayment;
        public decimal TotalPayment
        {
            get => Math.Round(totalPayment, 2);
            set => totalPayment = value;
        }

        private decimal totalInterest;
        public decimal TotalInterest
        {
            get => Math.Round(totalInterest, 2);
            set => totalInterest = value;
        }

        public string Email { get; set; } = null!;
        public string Message { get; set; } = "loan successful";
    }
}
