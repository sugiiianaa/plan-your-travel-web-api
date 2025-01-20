using MediatR;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.FlightTransactions.Commands.CreateFlightTransaction
{
    public sealed record CreateFlightTransactionCommand(
        int PaymentMethod,
        int NumberOfSeatBooked,
        Guid FlightSeatClassId)
        : IRequest<Result<Guid>>;
}
