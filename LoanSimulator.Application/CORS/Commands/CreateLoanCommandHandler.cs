using LoanSimulator.Domain.Entities;
using LoanSimulator.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSimulator.Application.CORS.Commands
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public CreateLoanCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = new Loan
            {
                Amount = request.Amount,
                DurationMonths = request.DurationMonths,
                InterestRate = request.InterestRate,
                Email = request.Email,
                MonthlyPayment = CalculateMonthlyPayment(request.Amount, request.InterestRate, request.DurationMonths)
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync(cancellationToken);

            return loan.Id;
        }

        private decimal CalculateMonthlyPayment(decimal amount, double interestRate, int months)
        {
            double monthlyInterestRate = interestRate / 100 / 12;
            double payment = (double)amount * monthlyInterestRate / (1 - Math.Pow(1 + monthlyInterestRate, -months));
            return (decimal)payment;
        }
    }
}
