using Eximia.PaymentGateway.Domain.Transactions.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eximia.PaymentGateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CaptureTransaction([FromServices] IMediator mediator, [FromBody] CaptureTransactionCommand command)
        {
            var result = await mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(new { error = result.Error });
            return Ok(new { result.Value.Id });
        }
    }
}
