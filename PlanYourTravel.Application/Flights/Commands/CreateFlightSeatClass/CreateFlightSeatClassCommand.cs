using MediatR;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Flights.Commands.CreateFlightSeatClass
{
    public sealed record CreateFlightSeatClassCommand(
        Guid FlightScheduleId,
        IList<CreateFlightSeatClassItem> FlightSeatClassItem)
            : IRequest<Result<List<Guid>>>;

    public sealed record CreateFlightSeatClassItem(
        int SeatClassType,
        int Capacity,
        int SeatsBooked,
        int Price);
}
