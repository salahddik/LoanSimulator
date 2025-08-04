using LoanSimulator.Domain.Entities;
using LoanSimulator.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LoanSimulator.Application.Queries;

namespace LoanSimulator.Application.Commands
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, LoanSimulationResultDto>
    {
        private readonly ILoanRepository _loanRepository;
        private const double FixedInterestRate = 4.1; // must be to domain

        public CreateLoanCommandHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<LoanSimulationResultDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            // must be to domain
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

            await _loanRepository.AddAsync(loan, cancellationToken);
            await _loanRepository.SaveChangesAsync(cancellationToken);

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
