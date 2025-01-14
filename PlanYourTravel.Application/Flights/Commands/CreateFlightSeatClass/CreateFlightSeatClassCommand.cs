using MediatR;
using PlanYourTravel.Domain.Shared;

namespace PlanYourTravel.Application.Flights.Commands.CreateFlightSeatClass
{
    public sealed record CreateFlightSeatClassCommand(
        IList<CreateFlightSeatClassItem> FlightSeatClass) : IRequest<Result<List<Guid>>>;

    public sealed record CreateFlightSeatClassItem(
        Guid FlightScheduleId,
        int SeatClassType,
        int Capacity,
        int SeatsBooked,
        int Price);
}
