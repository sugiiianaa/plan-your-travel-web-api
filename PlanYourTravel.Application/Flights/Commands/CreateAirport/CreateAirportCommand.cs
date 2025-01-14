using MediatR;
using PlanYourTravel.Application.Commons;
using PlanYourTravel.Domain.Enums;

namespace PlanYourTravel.Application.Flights.Commands.CreateAirport
{
    public sealed record CreateAirportCommand(
        string Name,
        string Code,
        Guid LocationId,
        AirportFlightType FlightType) : IRequest<Result<Guid>>;
}
