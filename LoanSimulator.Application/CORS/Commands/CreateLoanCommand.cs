using MediatR;
using LoanSimulator.Application.CORS.Queries;

namespace LoanSimulator.Application.CORS.Commands
{
    public record CreateLoanCommand(
        decimal Amount,
        int DurationMonths,
        string Email
    ) : IRequest<LoanSimulationResultDto>;
}
