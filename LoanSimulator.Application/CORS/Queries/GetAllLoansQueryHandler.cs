using LoanSimulator.Application.CORS.Queries;
using LoanSimulator.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoanSimulator.Application.CORS.Queries
{
    public class GetAllLoansQueryHandler : IRequestHandler<GetAllLoansQuery, List<LoanSimulationResultDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllLoansQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoanSimulationResultDto>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            // Fetch all loans from database
            var loans = await _context.Loans.ToListAsync(cancellationToken);

            // Map entity to DTO (you can also use AutoMapper here if configured)
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
                    TotalInterest = (loan.MonthlyPayment * loan.DurationMonths) - loan.Amount,
                    Email = loan.Email,
                    Message = "loan retrieved"
                });
            }

            return loanDtos;
        }
    }
}
