using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanYourTravel.Application.Flights.Commands.CreateAirport;
using PlanYourTravel.WebApi.Models;

namespace PlanYourTravel.WebApi.Controllers
{
    [ApiController]
    [Route("api/airport")]
    public class AirportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AirportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-airport")]
        public async Task<IActionResult> AddAirport(
            [FromBody] AddAirportRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateAirportCommand(request.Name, request.Code, request.LocationId, request.FlightType);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(new { code = result.Error?.Code, message = result.Error?.Message });
            }

            return Created(string.Empty, new { Id = result.Value });
        }
    }
}
