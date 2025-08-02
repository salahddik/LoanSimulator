using LoanSimulator.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanSimulator.Application.Queries
{
    public class GetAllLoansQueryHandler : IRequestHandler<GetAllLoansQuery, List<LoanSimulationResultDto>>
    {
        private readonly ILoanRepository _loanRepository;

        public GetAllLoansQueryHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<List<LoanSimulationResultDto>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _loanRepository.GetAllAsync(cancellationToken);

            var loanDtos = new List<LoanSimulationResultDto>();
            foreach (var loan in loans)
            {
                loanDtos.Add(new LoanSimulationResultDto
                {
                    Amount = loan.Amount,
                    DurationMonths = loan.DurationMonths,
                    InterestRate = loan.InterestRate,
                    MonthlyPayment = loan.MonthlyPayment,
                    TotalPayment = loan.MonthlyPayment * loan.DurationMonths,
                    TotalInterest = loan.MonthlyPayment * loan.DurationMonths - loan.Amount,
                    Email = loan.Email,
                    Message = "loan retrieved"
                });
            }

            return loanDtos;
        }
    }
}
