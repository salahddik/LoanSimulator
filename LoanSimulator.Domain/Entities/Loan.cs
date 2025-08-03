namespace LoanSimulator.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int DurationMonths { get; set; }
        public double InterestRate { get; set; }
        public decimal MonthlyPayment { get; set; }
        public required string Email { get; set; }
    }
}
