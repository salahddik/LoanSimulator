using MediatR;

namespace LoanSimulator.Application.Queries
{
    public record GetAllLoansQuery() : IRequest<List<LoanSimulationResultDto>>;
}
