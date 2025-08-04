using LoanSimulator.Infrastructure.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LoanSimulator.Application.Common;

namespace LoanSimulator.Application.Queries
{
    public class GetAllLoansQueryHandler(ILoanRepository loanRepository) : IRequestHandler<GetAllLoansQuery, PagedResult<LoanSimulationResultDto>>
    {
        public async Task<PagedResult<LoanSimulationResultDto>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            // Fetch paged data
            var loans = await loanRepository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);

            // Fetch total count for pagination metadata
            var totalCount = await loanRepository.CountAsync(cancellationToken);

            // Map domain entities to DTOs
            var loanDtos = loans.Select(loan => new LoanSimulationResultDto(loan)).ToList();

            // Return paged result with metadata
            return new PagedResult<LoanSimulationResultDto>(loanDtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
