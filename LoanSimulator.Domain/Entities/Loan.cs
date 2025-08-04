namespace LoanSimulator.Domain.Entities
{
    public class Loan
    {
        private const double FixedInterestRate = 4.1;
        private const double MonthlyInterestRate = FixedInterestRate / 100 / 12;


        public Loan(decimal amount, int durationMonths, string email)
        {
            Amount = amount;
            DurationMonths = durationMonths;
            InterestRate = FixedInterestRate;
            MonthlyPayment = CalculateMonthlyPayment(amount, durationMonths);
            Email = email;
        }

        public int Id { get; private set; }
        public decimal Amount { get; private set; }
        public int DurationMonths { get; private set; }
        public double InterestRate { get; private set; }
        public decimal MonthlyPayment { get; private set; }
        public string Email { get; private set; }

        public decimal TotalPayment => MonthlyPayment * DurationMonths;
        public decimal TotalInterest => TotalPayment - Amount;

        private decimal CalculateMonthlyPayment(decimal amount, int durationMonths)
        {
            var payment = (double)amount * MonthlyInterestRate / (1 - Math.Pow(1 + MonthlyInterestRate, -durationMonths));
            var calculateMonthlyPayment = (decimal)payment;

            return calculateMonthlyPayment;
        }

    }
}
