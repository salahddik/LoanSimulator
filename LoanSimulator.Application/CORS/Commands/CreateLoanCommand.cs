using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSimulator.Application.CORS.Commands
{
    public record CreateLoanCommand(
        decimal Amount,
        int DurationMonths,
        double InterestRate,
        string Email) : IRequest<int>;
}
