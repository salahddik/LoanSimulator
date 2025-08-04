using Xunit;
using Moq;
using MediatR;
using LoanSimulator.API.Controllers;
using LoanSimulator.Application.Queries;
using LoanSimulator.Application.Commands;
using LoanSimulator.Application.Common;
using LoanSimulator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LoanSimulator.Tests
{
    public class LoansControllerTests
    {
        private readonly Mock<IMediator> _mediator = new();
        private readonly LoansController _controller;

        public LoansControllerTests()
        {
            _controller = new LoansController(_mediator.Object);
        }

        private static Loan CreateLoan() => new Loan(1000m, 200, "zettodik@gmail.com");

        [Fact]
        public async Task GetAllLoans_ReturnsOkWithItems()
        {
            var items = new List<LoanSimulationResultDto> { new(CreateLoan()), new(CreateLoan()) };
            _mediator.Setup(m => m.Send(It.IsAny<GetAllLoansQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new PagedResult<LoanSimulationResultDto>(items, 2, 1, 10));

            var result = await _controller.GetAllLoans();

            var ok = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<PagedResult<LoanSimulationResultDto>>(ok.Value);
            Assert.Equal(2, data.TotalCount);
            Assert.Equal(2, data.Items.Count);
        }

        [Fact]
        public async Task CreateLoan_ValidCommand_ReturnsOkWithDto()
        {
            var loan = CreateLoan();
            var dto = new LoanSimulationResultDto(loan);

            _mediator.Setup(m => m.Send(It.IsAny<CreateLoanCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(dto);

            var command = new CreateLoanCommand(1000m, 200, "zettodik@gmail.com");
            var result = await _controller.CreateLoan(command);

            var ok = Assert.IsType<OkObjectResult>(result);
            var returnedDto = Assert.IsType<LoanSimulationResultDto>(ok.Value);

            Assert.Equal(loan.Amount, returnedDto.Amount);
            Assert.Equal(loan.Email, returnedDto.Email);

            // Update here: add precision for decimal comparison
            Assert.Equal(loan.MonthlyPayment, returnedDto.MonthlyPayment, 2);
            Assert.Equal(loan.TotalPayment, returnedDto.TotalPayment, 2);
            Assert.Equal(loan.TotalInterest, returnedDto.TotalInterest, 2);
        }

        [Fact]
        public async Task CreateLoan_InvalidModelState_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Amount", "Required");
            var command = new CreateLoanCommand(0, 0, string.Empty);

            var result = await _controller.CreateLoan(command);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
