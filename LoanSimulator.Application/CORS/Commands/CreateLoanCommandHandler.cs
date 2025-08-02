using LoanSimulator.Domain.Entities;
using LoanSimulator.Infrastructure.Data;
using LoanSimulator.Application.CORS.Queries; // For LoanSimulationResultDto
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LoanSimulator.Application.CORS.Commands
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, LoanSimulationResultDto>
    {
        private readonly ApplicationDbContext _context;
        private const double FixedInterestRate = 4.1;

        public CreateLoanCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoanSimulationResultDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            // Use fixed interest rate
            var monthlyPayment = LoanCalculator.CalculateMonthlyPayment(request.Amount, FixedInterestRate, request.DurationMonths);
            var totalPayment = monthlyPayment * request.DurationMonths;
            var totalInterest = totalPayment - request.Amount;

            var loan = new Loan
            {
                Amount = request.Amount,
                DurationMonths = request.DurationMonths,
                InterestRate = FixedInterestRate,
                Email = request.Email,
                MonthlyPayment = monthlyPayment
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync(cancellationToken);

            return new LoanSimulationResultDto
            {
                Amount = loan.Amount,
                DurationMonths = loan.DurationMonths,
                InterestRate = loan.InterestRate,
                MonthlyPayment = loan.MonthlyPayment,
                TotalPayment = totalPayment,
                TotalInterest = totalInterest,
                Email = loan.Email,
                Message = "loan successful"
            };
        }
    }
}
