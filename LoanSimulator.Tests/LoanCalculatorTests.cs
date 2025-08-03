using Xunit;
using LoanSimulator.Domain.Entities;

namespace LoanSimulator.Tests.Domain
{
    public class LoanCalculatorTests
    {
        [Fact]
        public void CalculateMonthlyPayment_WithZeroInterest_ReturnsPrincipalDividedByMonths()
        {
            // Arrange
            decimal amount = 1200m;
            double interestRate = 0;
            int durationMonths = 12;

            // Act
            var payment = LoanCalculator.CalculateMonthlyPayment(amount, interestRate, durationMonths);

            // Assert
            Assert.Equal(100m, payment);
        }

        [Fact]
        public void CalculateMonthlyPayment_WithPositiveInterest_ReturnsCorrectPayment()
        {
            // Arrange
            decimal amount = 10000m;
            double interestRate = 4.1;
            int durationMonths = 12;

            // Act
            var payment = LoanCalculator.CalculateMonthlyPayment(amount, interestRate, durationMonths);

            // Assert
            // The expected value can be calculated independently or from known good output
            Assert.InRange(payment, 850m, 860m); // Approximate expected value range
        }

        [Fact]
        public void CalculateMonthlyPayment_WithOneMonthDuration_ReturnsFullAmountPlusInterest()
        {
            // Arrange
            decimal amount = 10000m;
            double interestRate = 4.1;
            int durationMonths = 1;

            // Act
            var payment = LoanCalculator.CalculateMonthlyPayment(amount, interestRate, durationMonths);

            // Assert
            Assert.True(payment > amount); // Should be more than principal due to interest
        }
    }
}
