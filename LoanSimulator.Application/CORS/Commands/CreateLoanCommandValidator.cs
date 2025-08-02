using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSimulator.Application.CORS.Commands
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");
            RuleFor(x => x.DurationMonths).GreaterThan(0).WithMessage("Duration must be greater than zero.");
            RuleFor(x => x.InterestRate).InclusiveBetween(0.1, 100).WithMessage("Interest rate must be between 0.1 and 100.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        }
    }
}
