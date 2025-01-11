using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanYourTravel.Application.Flights.Commands.CreateFlightSchedule;
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

        // GET : /api/flight/flight-schedule
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

        // POST : /api/flight/flight-schedule
        [HttpPost("flight-schedule")]
        public async Task<IActionResult> CreateFlightSchedule(
            [FromBody] CreateFlightScheduleRequest request,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var command = new CreateFlightScheduleCommand(
                request.FlightSchedules.Select(fs =>
                new CreateFlightScheduleItem(
                    fs.FlightNumber,
                    fs.DepartureDateTime,
                    fs.ArrivalDateTime,
                    fs.DepartureAirportId,
                    fs.ArrivalAirportId,
                    fs.AirlineId)).ToList()
                    );

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(new { result.Error.Message });
            }

            return Ok(new { CreatedIds = result.Value });
        }
    }
}
