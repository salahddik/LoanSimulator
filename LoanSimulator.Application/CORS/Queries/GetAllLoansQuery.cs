using MediatR;

namespace LoanSimulator.Application.CORS.Queries
{
    public record GetAllLoansQuery() : IRequest<List<LoanSimulationResultDto>>;
}
