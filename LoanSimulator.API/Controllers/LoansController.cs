using LoanSimulator.Application.CORS.Commands;
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

        // POST: api/Loans
        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int newLoanId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetLoanById), new { id = newLoanId }, new { Id = newLoanId });
        }

        // Optional: simple GET by id endpoint
        [HttpGet("{id}")]
        public IActionResult GetLoanById(int id)
        {
            // You can implement a query handler to get the loan by id.
            // For now, just return 404 as placeholder.
            return NotFound();
        }
    }
}
