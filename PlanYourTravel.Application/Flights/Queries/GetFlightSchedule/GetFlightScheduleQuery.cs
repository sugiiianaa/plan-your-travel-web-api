using MediatR;
using PlanYourTravel.Application.Dtos;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Flights.Queries.GetFlightSchedule
{
    public sealed record GetFlightScheduleQuery(
        DateTime DepartureDate,
        Guid DepartureAirportId,
        Guid ArrivalAirportId,
        Guid LastSeendId,
        int PageSize) : IRequest<Result<FlightSchedulesPageDto>>;
}
