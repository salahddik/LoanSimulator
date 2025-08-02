using LoanSimulator.Application.CORS.Commands;
using LoanSimulator.Application.CORS.Queries;
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

        // POST api/Loans
        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            LoanSimulationResultDto result = await _mediator.Send(command);

            return Ok(result); // Return simulation result only, no ID
        }

        // GET api/Loans
        [HttpGet]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _mediator.Send(new GetAllLoansQuery());
            return Ok(loans);
        }
    }
}
