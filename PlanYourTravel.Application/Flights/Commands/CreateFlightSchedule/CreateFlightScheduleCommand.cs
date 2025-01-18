using MediatR;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Flights.Commands.CreateFlightSchedule
{
    public sealed record CreateFlightScheduleCommand
    (
        IList<CreateFlightScheduleItem> FlightSchedules)
            : IRequest<Result<List<Guid>>>;

    public sealed record CreateFlightScheduleItem(
        string FlightNumber,
        DateTime DepartureDateTime,
        DateTime ArrivalDateTime,
        Guid DepartureAirportId,
        Guid ArrivalAirportId,
        Guid AirlineId);
}
