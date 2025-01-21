using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanYourTravel.Application.Flights.Commands.CreateAirport;
using PlanYourTravel.WebApi.Helper;
using PlanYourTravel.WebApi.Models.Request;
using PlanYourTravel.WebApi.Models.Response;

namespace PlanYourTravel.WebApi.Controllers
{
    [ApiController]
    [Route("api/airport")]
    public class AirportController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        // POST /api/airport/add-airport
        [Authorize(Roles = "Admin")]
        [HttpPost("add-airport")]
        public async Task<IActionResult> AddAirport(
            [FromBody] AddAirportRequest request,
            CancellationToken cancellationToken)
        {
            var command = new CreateAirportCommand(
                request.Name,
                request.Code,
                request.LocationId,
                request.FlightType);

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(ResponseFormatterHelper<AddAirportResponse?>.FormatFailedResponse(result.Error!.ErrorCode));
            }

            var response = new AddAirportResponse
            {
                AirportId = result.Value
            };

            return Created(string.Empty, ResponseFormatterHelper<AddAirportResponse>.FormatSuccessResponse(response));
        }
    }
}
