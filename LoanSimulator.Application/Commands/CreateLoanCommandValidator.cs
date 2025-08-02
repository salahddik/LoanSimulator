using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSimulator.Application.Commands
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator()
        {
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(1000).WithMessage("Amount must be greater than or equal to 1000 MAD.");
            RuleFor(x => x.DurationMonths).GreaterThan(0).WithMessage("Duration must be greater than zero.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        }
    }
}
