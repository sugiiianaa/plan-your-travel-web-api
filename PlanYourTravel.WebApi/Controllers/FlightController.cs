using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanYourTravel.Application.Flights.Commands.CreateFlightSchedule;
using PlanYourTravel.Application.Flights.Commands.CreateFlightSeatClass;
using PlanYourTravel.Application.Flights.Queries.GetFlightSchedule;
using PlanYourTravel.WebApi.Models.Request;

namespace PlanYourTravel.WebApi.Controllers
{
    [ApiController]
    [Route("api/flight")]
    public class FlightController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // GET : /api/flight/flight-schedule
        [HttpGet("flight-schedule")]
        public async Task<IActionResult> GetFlightSchedule(
            [FromQuery] GetFlightScheduleRequest request,
            CancellationToken cancellationToken)
        {
            var query = new GetFlightScheduleQuery(
                request.DepartureDate,
                request.DepartFrom,
                request.ArriveAt,
                request.LastSeenId,
                request.PageSize);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        // POST : /api/flight/flight-schedule
        [Authorize(Roles = "Admin,SuperUser")]
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
                    fs.AirlineId))
                .ToList());

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(new { CreatedIds = result.Value });
        }

        // POST : /api/flight/flight-seat-class
        [Authorize(Roles = "Admin, SuperUser")]
        [HttpPost("flight-seat-class")]
        public async Task<IActionResult> CreateFlightSeatClass(
            [FromBody] CreateFlightSeatClassRequest request,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var command = new CreateFlightSeatClassCommand(
                request.FlightScheduleId,
                request.FlightSeatClassModels.Select(fsc =>
                new CreateFlightSeatClassItem(
                    fsc.SeatClassType,
                    fsc.Capacity,
                    fsc.SeatBooked ?? 0,
                    fsc.Price))
                .ToList());

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(new { CreatedIds = result.Value });
        }
    }
}
