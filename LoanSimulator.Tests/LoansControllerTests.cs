using Xunit;
using Moq;
using MediatR;
using LoanSimulator.API.Controllers;
using LoanSimulator.Application.Queries;
using LoanSimulator.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LoanSimulator.Tests
{
    public class LoansControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly LoansController _controller;

        public LoansControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new LoansController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllLoans_ReturnsPagedResult_Ok()
        {
            var items = new List<LoanSimulationResultDto>
            {
                new LoanSimulationResultDto
                {
                    Amount = 5000,
                    DurationMonths = 12,
                    InterestRate = (double)4.1m,
                    MonthlyPayment = 430,
                    TotalPayment = 5160,
                    TotalInterest = 160,
                    Email = "user@example.com",
                    Message = "loan retrieved"
                }
            };

            int totalCount = 1;
            int pageNumber = 1;
            int pageSize = 10;

            var pagedResult = new PagedResult<LoanSimulationResultDto>(items, totalCount, pageNumber, pageSize);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllLoansQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(pagedResult);

            var result = await _controller.GetAllLoans(pageNumber, pageSize);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPagedResult = Assert.IsType<PagedResult<LoanSimulationResultDto>>(okResult.Value);
            Assert.Equal(totalCount, returnedPagedResult.TotalCount);
            Assert.Equal(pageNumber, returnedPagedResult.PageNumber);
            Assert.Equal(pageSize, returnedPagedResult.PageSize);
            Assert.Single(returnedPagedResult.Items);
        }
    }
}
