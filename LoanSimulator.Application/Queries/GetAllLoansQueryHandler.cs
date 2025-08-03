using LoanSimulator.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LoanSimulator.Application.Common;

namespace LoanSimulator.Application.Queries
{
    public class GetAllLoansQueryHandler : IRequestHandler<GetAllLoansQuery, PagedResult<LoanSimulationResultDto>>
    {
        private readonly ILoanRepository _loanRepository;

        public GetAllLoansQueryHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<PagedResult<LoanSimulationResultDto>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            // Fetch paged data
            var loans = await _loanRepository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);

            // Fetch total count for pagination metadata
            var totalCount = await _loanRepository.CountAsync(cancellationToken);

            // Map domain entities to DTOs
            var loanDtos = loans.Select(loan => new LoanSimulationResultDto
            {
                Amount = loan.Amount,
                DurationMonths = loan.DurationMonths,
                InterestRate = loan.InterestRate,
                MonthlyPayment = loan.MonthlyPayment,
                TotalPayment = loan.MonthlyPayment * loan.DurationMonths,
                TotalInterest = loan.MonthlyPayment * loan.DurationMonths - loan.Amount,
                Email = loan.Email,
                Message = "loan retrieved"
            }).ToList();

            // Return paged result with metadata
            return new PagedResult<LoanSimulationResultDto>(loanDtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
