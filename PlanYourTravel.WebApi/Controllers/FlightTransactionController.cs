using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanYourTravel.Application.FlightTransactions.Commands.CreateFlightTransaction;
using PlanYourTravel.Application.FlightTransactions.Commands.PayFlightTransaction;
using PlanYourTravel.WebApi.Models;

namespace PlanYourTravel.WebApi.Controllers
{
    [ApiController]
    [Route("api/transaction/flight")]
    public class FlightTransactionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // POST : /api/transaction/flight/create-transaction
        [Authorize]
        [HttpPost("create-transaction")]
        public async Task<IActionResult> CreateFlightTransaction(
            [FromBody] CreateFlightTransactionRequest request,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var command = new CreateFlightTransactionCommand(
                request.PaymentMethod,
                request.NumberOfSeatBooked,
                request.FlightSeatClassId);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }


        // POST : /api/transaction/flight/pay-transaction
        [Authorize(Roles = "Admin")]
        [HttpPost("pay-transaction")]
        public async Task<IActionResult> PayTransaction(
            [FromQuery] Guid transactionId,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var command = new PayFlightTransactionCommand(transactionId);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
