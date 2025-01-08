using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanYourTravel.Application.Flights.Queries.GetFlightSchedule;
using PlanYourTravel.WebApi.Models;

namespace PlanYourTravel.WebApi.Controllers
{
    [ApiController]
    [Route("api/flight")]
    public class FlightController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FlightController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("flight-schedule")]
        public async Task<IActionResult> GetFlightSchedule(
            [FromQuery] GetFlightScheduleRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetFlightScheduleQuery(request.DepartureDate, request.DepartFrom, request.ArriveAt);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { result.Error.Message });
            }

            return Ok(result.Value);
        }

    }
}
