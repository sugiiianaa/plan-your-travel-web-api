using MediatR;
using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Flights.Commands.CreateAirport
{
    public sealed record CreateAirportCommand(
        string Name,
        string Code,
        Guid LocationId,
        AirportFlightType FlightType) : IRequest<Result<Guid>>;
}
