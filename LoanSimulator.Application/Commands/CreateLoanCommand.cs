using MediatR;
using LoanSimulator.Application.Queries;

namespace LoanSimulator.Application.Commands
{
    public record CreateLoanCommand(
        decimal Amount,
        int DurationMonths,
        string Email
    ) : IRequest<LoanSimulationResultDto>;
}
