using Xunit;
using FluentValidation.TestHelper;
using LoanSimulator.Application.Commands;

public class CreateLoanCommandValidatorTests
{
    private readonly CreateLoanCommandValidator _validator;

    public CreateLoanCommandValidatorTests()
    {
        _validator = new CreateLoanCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Amount_Is_Less_Than_1000()
    {
        var command = new CreateLoanCommand(999, 12, "test@example.com");
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Amount)
              .WithErrorMessage("Amount must be greater than or equal to 1000 MAD.");
    }

    [Fact]
    public void Should_Have_Error_When_DurationMonths_Is_Zero_Or_Less()
    {
        var command = new CreateLoanCommand(1500, 0, "test@example.com");
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.DurationMonths)
              .WithErrorMessage("Duration must be greater than zero.");
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new CreateLoanCommand(1500, 12, "invalid-email");
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Email)
              .WithErrorMessage("A valid email is required.");
    }

    [Fact]
    public void Should_Not_Have_Error_For_Valid_Command()
    {
        var command = new CreateLoanCommand(1500, 12, "test@example.com");
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
