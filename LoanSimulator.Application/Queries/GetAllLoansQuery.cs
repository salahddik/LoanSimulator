using LoanSimulator.Application.Common;
using MediatR;

namespace LoanSimulator.Application.Queries
{
    public record GetAllLoansQuery(int PageNumber, int PageSize) : IRequest<PagedResult<LoanSimulationResultDto>>;
}
