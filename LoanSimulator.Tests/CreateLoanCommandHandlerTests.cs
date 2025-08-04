using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using LoanSimulator.Application.Commands;
using LoanSimulator.Infrastructure.Repositories;
using LoanSimulator.Domain.Entities;

namespace LoanSimulator.Tests
{
    public class CreateLoanCommandHandlerTests
    {
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly CreateLoanCommandHandler _handler;

        public CreateLoanCommandHandlerTests()
        {
            _loanRepositoryMock = new Mock<ILoanRepository>();
            _handler = new CreateLoanCommandHandler(_loanRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_CalculatesAndSavesLoanCorrectly()
        {
            var command = new CreateLoanCommand(10000m, 12, "test@example.com");

            _loanRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _loanRepositoryMock
                .Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1)
                .Verifiable();

            var result = await _handler.Handle(command, CancellationToken.None);

            _loanRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Loan>(loan =>
                loan.Amount == command.Amount &&
                loan.DurationMonths == command.DurationMonths &&
                loan.Email == command.Email &&
                loan.InterestRate == 4.1
            ), It.IsAny<CancellationToken>()), Times.Once);

            _loanRepositoryMock.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            // Calculate expected monthly payment same way as Loan class does
            decimal expectedMonthlyPayment = CalculateMonthlyPayment(command.Amount, command.DurationMonths);
            decimal expectedTotalPayment = expectedMonthlyPayment * command.DurationMonths;
            decimal expectedTotalInterest = expectedTotalPayment - command.Amount;

            Assert.Equal(command.Amount, result.Amount);
            Assert.Equal(command.DurationMonths, result.DurationMonths);
            Assert.Equal(4.1, result.InterestRate);
            Assert.Equal(expectedMonthlyPayment, result.MonthlyPayment, 2);
            Assert.Equal(expectedTotalPayment, result.TotalPayment, 2);
            Assert.Equal(expectedTotalInterest, result.TotalInterest, 2);
            Assert.Equal(command.Email, result.Email);
            Assert.Equal("loan successful", result.Message);
        }

        // Helper method mimicking Loan.CalculateMonthlyPayment
        private decimal CalculateMonthlyPayment(decimal amount, int durationMonths)
        {
            const double fixedInterestRate = 4.1;
            double monthlyInterestRate = fixedInterestRate / 100 / 12;
            double payment = (double)amount * monthlyInterestRate / (1 - System.Math.Pow(1 + monthlyInterestRate, -durationMonths));
            return (decimal)payment;
        }
    }
}
