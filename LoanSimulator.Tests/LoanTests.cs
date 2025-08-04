using LoanSimulator.Domain.Entities;
using Xunit;

namespace LoanSimulator.Tests
{
    public class LoanTests
    {
        [Theory]
        [InlineData(1000, 12, 85.20)]  // updated expected monthly payments
        [InlineData(2000, 24, 86.94)]
        [InlineData(5000, 60, 92.31)]
        public void Constructor_ShouldCalculateMonthlyPayment(decimal amount, int durationMonths, decimal expectedMonthlyPayment)
        {
            var loan = new Loan(amount, durationMonths, "test@example.com");

            Assert.Equal(amount, loan.Amount);
            Assert.Equal(durationMonths, loan.DurationMonths);
            Assert.Equal(4.1, loan.InterestRate);

            // Round actual before comparing
            Assert.Equal(expectedMonthlyPayment, Math.Round(loan.MonthlyPayment, 2));
        }

        [Theory]
        [InlineData(1000, 12)]
        [InlineData(2000, 24)]
        [InlineData(5000, 60)]
        public void TotalPayment_ShouldBeMonthlyPaymentTimesDuration(decimal amount, int durationMonths)
        {
            var loan = new Loan(amount, durationMonths, "test@example.com");
            var expectedTotalPayment = Math.Round(loan.MonthlyPayment * durationMonths, 2);

            Assert.Equal(expectedTotalPayment, Math.Round(loan.TotalPayment, 2));
        }

        [Theory]
        [InlineData(1000, 12)]
        [InlineData(2000, 24)]
        [InlineData(5000, 60)]
        public void TotalInterest_ShouldBeTotalPaymentMinusAmount(decimal amount, int durationMonths)
        {
            var loan = new Loan(amount, durationMonths, "test@example.com");
            var expectedTotalInterest = Math.Round(loan.TotalPayment - amount, 2);

            Assert.Equal(expectedTotalInterest, Math.Round(loan.TotalInterest, 2));
        }
    }
}
