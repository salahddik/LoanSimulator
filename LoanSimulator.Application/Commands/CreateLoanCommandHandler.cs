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

        public CreateLoanCommandHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<LoanSimulationResultDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = new Loan(request.Amount, request.DurationMonths, request.Email);

            await _loanRepository.AddAsync(loan, cancellationToken);
            await _loanRepository.SaveChangesAsync(cancellationToken);

            return new LoanSimulationResultDto(loan);
        }
    }
}
