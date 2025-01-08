using MediatR;
using PlanYourTravel.Domain.Dtos;
using PlanYourTravel.Domain.Shared;

namespace PlanYourTravel.Application.Flights.Queries.GetFlightSchedule
{
    public sealed record GetFlightScheduleQuery(
        DateTime DepartureDate,
        Guid DepartureAirportId,
        Guid ArrivalAirportId) : IRequest<Result<IList<FlightScheduleDto>>>;
}
