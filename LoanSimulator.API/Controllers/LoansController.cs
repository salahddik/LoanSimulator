using LoanSimulator.Application.Commands;
using LoanSimulator.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoanSimulator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            LoanSimulationResultDto result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("LoanGetAllData")]
        public async Task<IActionResult> GetAllLoans([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var loans = await _mediator.Send(new GetAllLoansQuery(pageNumber, pageSize));
            return Ok(loans);
        }
    }
}
